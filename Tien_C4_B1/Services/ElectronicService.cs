using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class ElectronicService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Electronic> electronicRepo { get; set; }

        public ElectronicService()
        {
            electronicRepo = unitOfWork.ElectronicRepository;
        }

        public bool isExistId(string id)
        {
            if (electronicRepo.Get(id) != null)
                    return true;
            return false;
        }

        public void Add(Electronic electric)
        {
            electronicRepo.Add(electric);
        }

        public void Remove(int idx)
        {
            //electronicRepo.Remove(electronicRepo.lstElectronic[idx]);
            //electronicRepo.lstElectronic.Remove(electronicRepo.lstElectronic[idx]);
        }

        public Electronic getProductById(string id)
        {
            return electronicRepo.Get(id);
        }

        public List<Electronic> Gets()
        {
            return electronicRepo.Gets();
        }

        public void Update(Electronic electric)
        {
            electronicRepo.Update(electric);
        }
    }
}
