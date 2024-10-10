using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class SalesSlipService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<SalesSlip> salesSlipRepo { get; set; }

        public SalesSlipService()
        {
            salesSlipRepo = unitOfWork.SalesSlipRepository;
        }

        public bool isExist(string id)
        {
            foreach (var item in salesSlipRepo.Gets())
                if (string.Compare(item.Id, id, true) == 0)
                    return true;
            return false;
        }

        public void Add(SalesSlip saleSlip)
        {
            int count = saleSlip.lstSalesDetail.Count - 1;
            saleSlip.TotalDiscount += saleSlip.lstSalesDetail[count].Discount;
            salesSlipRepo.Add(saleSlip);
        }

        public SalesSlip Get(string id)
        {
            return salesSlipRepo.Get(id);
        }

        public List<SalesSlip> Gets()
        {
            return salesSlipRepo.Gets();
        }

        public string GetId()
        {
            string id = null;
            do
            {
                if (Gets().Count >= 9)
                    id = "PBH" + (Gets().Count + 1).ToString();
                else
                    id = "PBH0" + (Gets().Count + 1).ToString();
            } while (Get(id) != null);
            return id;
        }

    }
}
