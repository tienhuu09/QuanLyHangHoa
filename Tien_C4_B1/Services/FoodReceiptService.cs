using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class FoodReceiptService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<FoodReceipt> foodReceiptRepo { get; set; }
        public ExpDateRepository expDateRepo { get; set; }

        public FoodReceiptService()
        {
            foodReceiptRepo = unitOfWork.FoodReceiptRepo;
            expDateRepo = new ExpDateRepository();
        }

        public void Add(FoodReceipt foodReceipt)
        {
            foodReceiptRepo.Add(foodReceipt);
        }

        public void AddListFoodReceipt(List<FoodReceipt> foodReceipt)
        {
            foreach (var item in foodReceipt)
            {
                Add(item);
            }
        }

        public void SaveProductExpDate()
        {
            expDateRepo.SaveFile(foodReceiptRepo.Gets());
        }

        public List<FoodReceipt> Gets()
        {
            return foodReceiptRepo.Gets();
        }

        public void RepairFoodReceipt(Invoice invoice)
        {
            foreach (var invoiceDetail in invoice.lstInvoiceDt)
            {
                int quantityInvoice = invoiceDetail.Quantity;
                foreach (var item in unitOfWork.FoodReceiptRepo.Gets())
                {
                    if (string.Compare(item.IdProduct, invoiceDetail.IdProduct, true) == 0 && item.Status == true && item.ExpQuan != item.Quantity)
                    {
                        if (item.ExpQuan < item.Quantity)
                        {
                            int temp = item.Quantity - item.ExpQuan;
                            if (temp > quantityInvoice)
                            {
                                item.ExpQuan += quantityInvoice;
                                foodReceiptRepo.Update(item);
                                break;
                            }
                            else
                            {
                                quantityInvoice -= temp;
                                item.ExpQuan = item.Quantity;
                                foodReceiptRepo.Update(item);
                            }
                            if (quantityInvoice == 0)
                                break;
                        }
                    }
                }
            }
            
        }

    }
}
