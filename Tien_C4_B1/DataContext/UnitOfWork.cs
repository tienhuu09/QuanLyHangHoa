 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class UnitOfWork
    {
        private static IRepository<Account> _AccountRepo;
        private static IRepository<Electronic> _ElectronicRepo;
        private static IRepository<Porcelain> _PorcelainRepo;
        private static IRepository<Invoice> _InvoiceRepo;
        private static IRepository<Receipt> _ReceiptRepo;
        private static IRepoInventorySale<InventorySale> _InvenSaleRepo;
        private static IRepository<SalesSlip> _SalesSlipRepo;
        private static IRepository<Card> _CardRepository;
        private static IRepository<FoodReceipt> _FoodReceiptRepo;
        private static IRepository<Food> _FoodRepo;
        private static IRepository<Role> _RoleRepo;
        private static IRepository<OutOfStock> _OutOfStockRepo;
        private static IRepoCustomer<Customer> _CustomerRepo;
        private static IRepoInventory<Inventory> _InventoryRepo;

        public IRepository<Account> AccountRepository
        {
            get
            {
                if (_AccountRepo == null)
                    _AccountRepo = new AccountRepository();
                return _AccountRepo;
            }
            set
            {
                _AccountRepo = value;
            }
        }

        public IRepository<Electronic> ElectronicRepository
        {
            get
            {
                if (_ElectronicRepo == null)
                    _ElectronicRepo = new ElectronicRepository();
                return _ElectronicRepo;
            }
            set
            {
                _ElectronicRepo = value;
            }
        }

        public IRepoInventory<Inventory> InventoryRepository
        {
            get
            {
                if (_InventoryRepo == null)
                    _InventoryRepo = new InventoryRepository();
                return _InventoryRepo;
            }
            set
            {
                _InventoryRepo = value;
            }
        }

        public IRepository<Porcelain> PorcelainRepository
        {
            get
            {
                if (_PorcelainRepo == null)
                    _PorcelainRepo = new PorcelainRepository();
                return _PorcelainRepo;
            }
            set
            {
                _PorcelainRepo = value;
            }
        }

        public IRepository<Invoice> InvoiceRepository
        {
            get
            {
                if (_InvoiceRepo == null)
                    _InvoiceRepo = new InvoiceRepository();
                return _InvoiceRepo;
            }
            set
            {
                _InvoiceRepo = value;
            }
        }

        public IRepository<Receipt> ReceiptRepository
        {
            get
            {
                if (_ReceiptRepo == null)
                    _ReceiptRepo = new ReceiptRepository();
                return _ReceiptRepo;
            }
            set
            {
                _ReceiptRepo = value;
            }
        }

        public IRepoInventorySale<InventorySale> InventorySaleRepository
        {
            get
            {
                if (_InvenSaleRepo == null)
                    _InvenSaleRepo = new InventorySaleRepo();
                return _InvenSaleRepo;
            }
            set
            {
                _InvenSaleRepo = value;
            }
        }

        public IRepository<SalesSlip> SalesSlipRepository
        {
            get
            {
                if (_SalesSlipRepo == null)
                    _SalesSlipRepo = new SalesSlipRepository();
                return _SalesSlipRepo;
            }
            set
            {
                _SalesSlipRepo = value;
            }
        }

        public IRepository<Card> CardRepository
        {
            get
            {
                if (_CardRepository == null)
                    _CardRepository = new CardRepository();
                return _CardRepository;
            }
            set
            {
                _CardRepository = value;
            }
        }

        public IRepoCustomer<Customer> CustomerRepository
        {
            get
            {
                if (_CustomerRepo == null)
                    _CustomerRepo = new CustomerRepository();
                return _CustomerRepo;
            }
            set
            {
                _CustomerRepo = value;
            }
        }
 
        public IRepository<FoodReceipt> FoodReceiptRepo
        {
            get
            {
                if (_FoodReceiptRepo == null)
                    _FoodReceiptRepo = new FoodReceiptRepository();
                return _FoodReceiptRepo;
            }
            set
            {
                _FoodReceiptRepo = value;
            }
        }

        public IRepository<Food> FoodRepository
        {
            get
            {
                if (_FoodRepo == null)
                    _FoodRepo = new FoodRepository();
                return _FoodRepo;
            }
            set
            {
                _FoodRepo = value;
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                if (_RoleRepo == null)
                    _RoleRepo = new RoleRepository();
                return _RoleRepo;
            }
            set
            {
                _RoleRepo = value;
            }
        }

        public IRepository<OutOfStock> OutOfStockRepo
        {
            get
            {
                if (_OutOfStockRepo == null)
                    _OutOfStockRepo = new OutOfStockRepository();
                return _OutOfStockRepo;
            }
            set
            {
                _OutOfStockRepo = value;
            }
        }

        #region LoadRoelToAcc
        public void LoadAccountRoleByIdRole()
        {
            foreach (var item in AccountRepository.Gets())
            {
                foreach (var item2 in RoleRepository.Gets())
                {
                    if (string.Compare(item.IdRole, item2.IdRole, true) == 0)
                    {
                        item.Role = item2;
                        break; 
                    }
                }
            }
        }
        #endregion

        #region LoadProductToInventory
        public void LoadProductToInventory()
        {
            int idx = 0;
            Inventory Inventory = InventoryRepository.Get();
            var InventorySale = InventorySaleRepository.Gets();
            foreach (var item in Inventory.lstImport)
            {
                foreach (var food in FoodRepository.Gets())
                {
                    if (string.Compare(item.IdProduct, food.Id, true) == 0)
                    {
                        item.Product = food;
                        Inventory.lstExport[idx].Product = food;
                        Inventory.lstRemain[idx].Product = food;
                        InventorySale[idx].product = food;
                        continue;
                    }
                }
                foreach (var porcelain in PorcelainRepository.Gets())
                {
                    if (string.Compare(item.IdProduct, porcelain.Id, true) == 0)
                    {
                        item.Product = porcelain;
                        Inventory.lstExport[idx].Product = porcelain;
                        Inventory.lstRemain[idx].Product = porcelain;
                        InventorySale[idx].product = porcelain;
                        continue;
                    }
                }
                foreach (var electric in ElectronicRepository.Gets())
                {
                    if (string.Compare(item.IdProduct, electric.Id, true) == 0)
                    {
                        item.Product = electric;
                        Inventory.lstExport[idx].Product = electric;
                        Inventory.lstRemain[idx].Product = electric;
                        InventorySale[idx].product = electric;
                        continue;   
                    }
                }
                idx++;
            }
        }
        #endregion

        public void LoadCustomer()
        {
            foreach (var item in SalesSlipRepository.Gets())
            {
                foreach (var itemCus in CustomerRepository.Gets())
                {
                    if (string.Compare(item.customer.IdCard, itemCus.IdCard, true) == 0)
                    {
                        item.customer = itemCus;
                        break;
                    }
                }
            }
        }

        public static bool flag { get; set; } = false;
        public void DateFoodReceiptInValid()
        {
            var Inventory = InventoryRepository.Get();
            int idx = 0;
            foreach (var itemRemain in Inventory.lstRemain)
            {
                if (string.Compare(itemRemain.Product.Category, "Food", true) == 0)
                {
                    itemRemain.Quantity = Inventory.lstImport[idx].Quantity;
                    foreach (var item in FoodReceiptRepo.Gets())
                    {
                        if (string.Compare(itemRemain.IdProduct, item.IdProduct, true) == 0)
                        {
                            if (item.Status == true)
                            {
                                int quantity = item.ExpQuan;
                                itemRemain.Quantity -= quantity;
                            }
                            else
                                itemRemain.Quantity -= item.Quantity;
                        }
                    }
                }
                idx++;
            }
            flag = true;
        }

        public UnitOfWork()
        {
            LoadAccountRoleByIdRole();
            LoadProductToInventory();
            if (!flag)
                DateFoodReceiptInValid();
            Constants.QuantityUser = _AccountRepo.Gets().Count;

        }

    }
}
