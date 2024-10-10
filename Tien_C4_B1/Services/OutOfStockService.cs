using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class OutOfStockService
    {
        private static readonly UnitOfWork unitOfWork = new UnitOfWork();

        public IRepository<OutOfStock> outOfStockRepo { get; set; }

        public OutOfStockService()
        {
            outOfStockRepo = unitOfWork.OutOfStockRepo;
        }

    }
}
