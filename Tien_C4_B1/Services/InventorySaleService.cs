using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class InventorySaleService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepoInventorySale<InventorySale> inventorySaleRepo { get; set; }

        public InventorySaleService()
        {
            inventorySaleRepo = unitOfWork.InventorySaleRepository;
        }

        public void AddProduct(Product product)
        {
            InventorySale temp = new InventorySale(product);
            inventorySaleRepo.Add(temp);
            inventorySaleRepo.AddProduct(product);
        }

        public void AddProductFromStock(Invoice invoice)
        {
            foreach (var item in invoice.lstInvoiceDt)
            {
                foreach (var itemInven in inventorySaleRepo.Gets())
                {
                    if (string.Compare(itemInven.IdProduct, item.IdProduct, true) == 0)
                    {
                        itemInven.QuantityInvoice += item.Quantity;
                        itemInven.Remaining += item.Quantity;
                        inventorySaleRepo.Update(itemInven);
                        break;
                    }
                }
            }
        }

        public void AddSaleSlip(SalesSlip saleSlip)
        {
            foreach (var item in saleSlip.lstSalesDetail)
            {
                foreach (var itemInven in inventorySaleRepo.Gets())
                {
                    if (string.Compare(item.IdProduct, itemInven.IdProduct, true) == 0)
                    {
                        //itemInven.Remaining -= item.Quantity;
                        itemInven.QuantitySold += item.Quantity;
                        inventorySaleRepo.Update(itemInven);
                        break;
                    }
                }
            }
        }

        public double getQuantityProductRemaing(string id)
        {
            foreach (var item in inventorySaleRepo.Gets())
                return item.Remaining;
            return 0;
        }

        public InventorySale GetInventorySale(Product product)
        {
            foreach (var item in inventorySaleRepo.Gets())
                if (string.Compare(item.product.Id, product.Id, true) == 0)
                    return item;
            return null;
        }

        public List<InventorySale> Gets()
        {
            return inventorySaleRepo.Gets();
        }
    }
}
