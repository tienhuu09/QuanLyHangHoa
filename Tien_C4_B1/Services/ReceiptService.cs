using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class ReceiptService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Receipt> receiptRepo { get; set; }

        public ReceiptService()
        {
            receiptRepo = unitOfWork.ReceiptRepository;
        }

        public void Add(Receipt receipt)
        {
            receiptRepo.Add(receipt);
        }

        public bool isExistId(string id)
        {
            foreach (var item in receiptRepo.Gets())
                if (string.Compare(item.Id, id, true) == 0)
                    return true;
            return false;
        }

        public Receipt Get(string id)
        {
            return receiptRepo.Get(id);
        }

        public List<Receipt> Gets()
        {
            return receiptRepo.Gets();
        }

        public string GetId()
        {
            string id = null;
            do
            {
                if (Gets().Count >= 9)
                    id = "PNK" + (Gets().Count + 1).ToString();
                else
                    id = "PNK0" + (Gets().Count + 1).ToString();
            } while (isExistId(id));
            return id;
        }
    }
 }
