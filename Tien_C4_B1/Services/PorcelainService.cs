using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class PorcelainService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Porcelain> porcelainRepo { get; set; }

        public PorcelainService()
        {
            porcelainRepo = unitOfWork.PorcelainRepository;
        }

        public bool isExistId(string id)
        {
            foreach (var item in porcelainRepo.Gets())
                if (string.Compare(item.Id, id, true) == 0)
                    return true;
            return false;
        }

        public void Add(Porcelain porcelain)
        {
            porcelainRepo.Add(porcelain);
        }

        public void Remove(int idx)
        {
            //porcelainRepo.Remove(porcelainRepo.lstPorcelain[idx]);
            //porcelainRepo.lstPorcelain.Remove(porcelainRepo.lstPorcelain[idx]);
        }

        public Porcelain getProductById(string id)
        {
            foreach (var item in porcelainRepo.Gets())
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Porcelain> Gets()
        {
            return porcelainRepo.Gets();
        }

        public void Update(Porcelain porcelain)
        {
            porcelainRepo.Update(porcelain);
        }
    }
}
