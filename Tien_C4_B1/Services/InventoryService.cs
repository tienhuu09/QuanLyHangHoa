using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tien_C4_B1
{
    public class InventoryService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();

        public IRepoInventory<Inventory> inventoryRepo { get; set; }

        public InventoryService()
        {
            inventoryRepo = unitOfWork.InventoryRepository;
        }

        public void AddProduct(Product product)
        {
            ImportInventory import = new ImportInventory();
            import.IdProduct = product.Id;
            import.Product = product;
            //inventoryRepo.inventory.lstImport.Add(import);
            inventoryRepo.AddStockProduct(import);

            ExportInventory export = new ExportInventory();
            export.IdProduct = product.Id;
            export.Product = product;
            //inventoryRepo.inventory.lstExport.Add(export);
            inventoryRepo.AddOutOfStock(export);

            RemainingProduct remaining = new RemainingProduct();
            remaining.IdProduct = product.Id;
            remaining.Product = product;
            //inventoryRepo.inventory.lstRemain.Add(remaining);
            inventoryRepo.AddRemainingProduct(remaining);
        }

        public void AddReceipt(Receipt receipt)
        {
            foreach (var item in receipt.lstReceiptDt)
            {
                foreach (var stock in inventoryRepo.Get().lstImport)
                {
                    if (string.Compare(item.IdProduct, stock.IdProduct, true) == 0)
                    {
                        stock.Previous += stock.Recent;
                        stock.AmountPre += stock.AmountRecent;

                        stock.Recent = item.Quantity;
                        stock.AmountRecent = item.AmountPriceInput;

                        stock.Quantity = stock.Previous + stock.Recent;
                        stock.Total = stock.AmountPre + stock.AmountRecent;
                        stock.DateReceipt = receipt.CreateAt;

                        inventoryRepo.RepairReceipt(receipt, stock);
                    }
                }
            }
            AddReceiptToRemaining(receipt);
        }

        public void AddReceiptToRemaining(Receipt receipt)
        {
            foreach (var remain in inventoryRepo.Get().lstRemain)
            {
                foreach (var item in receipt.lstReceiptDt)
                {
                    if (string.Compare(remain.IdProduct, item.IdProduct, true) == 0)
                    {
                        remain.Quantity += item.Quantity;
                        inventoryRepo.AddFoodReceiptDetail(remain, item);
                    }
                }
            }
        }

        public void AddInvoice(Invoice invoice)
        {
            foreach (var item in invoice.lstInvoiceDt)
            {
                foreach (var outStock in inventoryRepo.Get().lstExport)
                {
                    if (string.Compare(item.IdProduct, outStock.IdProduct, true) == 0)
                    {
                        outStock.Previous += outStock.Recent;
                        outStock.AmountPre += outStock.AmountRecent;

                        outStock.Recent = item.Quantity;
                        outStock.AmountRecent = item.AmountPriceOutput;

                        outStock.Quantity = outStock.Previous + outStock.Recent;
                        outStock.Total = outStock.AmountPre + outStock.AmountRecent;
                        outStock.DateInvoice = invoice.CreateAt;

                        inventoryRepo.RepairInvoice(invoice, outStock);
                    }
                }
            }
            AddInvoiceToRemaining(invoice);
        }

        public void AddInvoiceToRemaining(Invoice invoice)
        {
            foreach (var item in invoice.lstInvoiceDt)
            {
                foreach (var remain in inventoryRepo.Get().lstRemain)
                {
                    if (string.Compare(remain.IdProduct, item.IdProduct, true) == 0)
                    {
                        inventoryRepo.ReduceRemainingProduct(remain, item);
                    }
                }
            }
        }

        //public void CalculateRemaining()
        //{
        //    int idx = 0;
        //    foreach (var item in inventoryRepo.Get().lstRemain)
        //    {
        //        item.Quantity = inventoryRepo.Get().lstImport[idx].Quantity - inventoryRepo.Get().lstExport[idx].Quantity;
        //        idx++;
        //        inventoryRepo.RepairRemaining(item);
        //    }
        //}

        public ImportInventory getProduct(int opt)
        {
            return inventoryRepo.Get().lstImport[opt];
        }

        public double getQuantityProductRemaing(string id)
        {
            foreach (var item in inventoryRepo.Get().lstRemain)
                if (string.Compare(item.IdProduct, id, true) == 0)
                    return item.Quantity;
            return 0;
        }

        public Inventory Get()
        {
            return inventoryRepo.Get();
        }
    }
}
