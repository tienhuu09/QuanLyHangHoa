using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class CustomerService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();
        public IRepoCustomer<Customer> customerRepo { get; set; }
        public CardSevice cardSevice { get; set; }

        public CustomerService()
        {
            customerRepo = unitOfWork.CustomerRepository;
            cardSevice = new CardSevice();
        }

        public void AddSale(SalesSlip salesSlip, Customer customer, bool useCard)
        {
            int flag = 0;
            if (cardSevice.isExistCustomer(customer.IdCard))
                flag = 1;
            CustomerDetail customerDt = new CustomerDetail();
            customerDt.IdSaleSlip = salesSlip.Id;
            customerDt.Quantity = salesSlip.Quantity;

            int useScore = 0;
            double totalMoneyUseScore = 0;
            double totalMoneyUseScore2 = 0;
            if (useCard)
            {
                useScore = Convert.ToInt32(customer.TotalScore);
                totalMoneyUseScore = useScore * 10.0;
                totalMoneyUseScore2 = useScore * 10.0;

                if (totalMoneyUseScore >= salesSlip.Total)
                {
                    double temp = totalMoneyUseScore2 - salesSlip.Total;
                    useScore = Convert.ToInt32(salesSlip.Total * 0.1);
                    int remainScore = Convert.ToInt32(temp * 0.1);

                    customer.UseScore += useScore;
                    customerDt.UseScore = useScore;
                    customerDt.DiscountByScore = salesSlip.Total;
                    customerDt.Total = 0;
                    salesSlip.TotalDiscount = salesSlip.Total;
                    salesSlip.Total = 0;
                }
                else
                {
                    customerDt.Total = salesSlip.Total - totalMoneyUseScore;
                    salesSlip.Total = customerDt.Total;
                    salesSlip.TotalDiscount = totalMoneyUseScore;
                    customer.UseScore += customer.TotalScore;
                    customerDt.UseScore = useScore;
                    customerDt.DiscountByScore = totalMoneyUseScore;
                    customer.TotalScore = 0;
                }
            }
            else
                customerDt.Total = salesSlip.Total;
            if (flag == 1)
                customerDt.Score = Convert.ToInt32(salesSlip.getScore);
            customerDt.createAt = salesSlip.CreateAt;

            foreach (var item in customerRepo.Gets())
            {
                if (string.Compare(item.IdCard, salesSlip.customer.IdCard, true) == 0)
                {
                    if (flag == 1)
                    {
                        item.TotalScore += customerDt.Score;
                        item.Card = item.CalcuScore;
                    }
                    item.lstCustomerDt.Add(customerDt);
                    customerRepo.AddCustomerDetail(item, customerDt);
                    return;
                }
            }

            customer.TotalScore += customerDt.Score;
            customer.lstCustomerDt.Add(customerDt);
            customerRepo.Add(customer);
        }

        public Customer getCustomer(string idCard)
        {
            foreach (var item in customerRepo.Gets())
                if (string.Compare(idCard, item.IdCard, true) == 0)
                    return item;
            return null;
        }

        public void Add(Customer customer)
        {
            foreach (var item in customerRepo.Gets())
            {
                if (string.Compare(item.IdCard, customer.IdCard, true) == 0)
                {
                    item.Card = "Member";
                    return;
                }
            }
            customerRepo.Add(customer);
        }

        public Customer Get(string id)
        {
            return customerRepo.Get(id);
        }

        public List<Customer> Gets()
        {
            return customerRepo.Gets();
        }
    }
}
