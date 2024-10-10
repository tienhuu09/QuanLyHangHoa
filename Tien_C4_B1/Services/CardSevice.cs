using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class CardSevice
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepository<Card> cardRepo { get; set; }

        public CardSevice()
        {
            cardRepo = unitOfWork.CardRepository;
        }

        public bool isExistCustomer(string idCard)
        {
            foreach (var item in cardRepo.Gets())
                if (string.Compare(item.Id, idCard, true) == 0)
                    return true;
            return false;
        }

        public bool isExistCard(string idCard)
        {
            foreach (var item in cardRepo.Gets())
                if (string.Compare(item.Id, idCard, true) == 0)
                    return true;
            return false;
        }

        public void Update(Customer customer, bool flag)
        {
            foreach (var item in Gets())
            {
                if (string.Compare(item.Id, customer.IdCard, true) == 0)
                {
                    if (flag == true)
                        item.Score = 0;
                    item.Score += customer.lstCustomerDt[customer.lstCustomerDt.Count - 1].Score;
                    item._Card = customer.CalcuScore;
                    cardRepo.Update(item);
                    return;
                }
            }
        }

        public void AddCard(Card card)
        {
            cardRepo.Add(card);
        }

        public Card Get(string idCard)
        {
            return cardRepo.Get(idCard);
        }

        public List<Card> Gets()
        {
            return cardRepo.Gets();
        }
    }
}
