using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class InvoiceService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Invoice> invoiceRepo { get; set; }

        public InvoiceService()
        {
            invoiceRepo = unitOfWork.InvoiceRepository;
        }

        public bool isExistId(string id)
        {
            if (invoiceRepo.Get(id) != null)
                return true;
            return false;
        }

        public void Add(Invoice invoice)
        {
            invoiceRepo.Add(invoice);
        }

        public Invoice Get(string id)
        {
            return invoiceRepo.Get(id);
        }

        public List<Invoice> Gets()
        {
            return invoiceRepo.Gets();
        }

        public string GetId()
        {
            string id = null;
            do
            {
                if (Gets().Count >= 9)
                    id = "PXK" + (Gets().Count + 1).ToString();
                else
                    id = "PXK0" + (Gets().Count + 1).ToString();
            } while (isExistId(id));
            return id;
        }
    }
}
