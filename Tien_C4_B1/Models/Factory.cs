using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Factory
    {
        public static Product CreateType(int type)
        {
            switch (type)
            {
                case 1:
                    return new Food();
                case 2:
                    return new Porcelain();
                case 3:
                    return new Electronic();
            }
            return null;
        }

        public static Product CreateTypeStr(string str)
        {
            switch (str)
            {
                case "Electric":
                    return new Electronic();
                case "Porcelain":
                    return new Porcelain();
                case "Food":
                    return new Food();
            }
            return null;
        }
    }
}
