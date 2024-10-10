using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class FoodService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Food> FoodRepository { get; set; }

        public FoodService()
        {
            FoodRepository = unitOfWork.FoodRepository;
        }

        public bool isExistId(string id)
        {
            if (FoodRepository.Get(id) != null)
                return true;
            return false;
        }

        public void Add(Food food)
        {
            FoodRepository.Add(food);
        }

        public void Remove(int idx)
        {
            //foodRepo.Remove(foodRepo.lstFood[idx]);
            //foodRepo.lstFood.Remove(foodRepo.lstFood[idx]);
        }

        public bool InValidExpired(DateTime mfgDate, DateTime expDate)
        {
            int result = DateTime.Compare(mfgDate, expDate);
            if (result > 0)
                return true;
            return false;
        }

        public Food getProductById(string id)
        {
            foreach (var item in FoodRepository.Gets())
                if (string.Compare(item.Id, id, true) == 0)
                    return item;
            return null;
        }

        public List<Food> Gets()
        {
            return FoodRepository.Gets();
        }

        public void Update(Food food)
        {
            FoodRepository.Update(food);
        }
    }
}
