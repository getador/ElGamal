using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElGamalCriptografic.Classes
{
    class SimpleNumberWorker
    {
        public static bool IsPrime(uint num)
        {
            if (num == 2) return true;
            if (num % 2 == 0) return false;

            for (int i = 3; i <= num / 2; i += 2)
            {
                if (num % i == 0) return false;
            }

            return true;
        }

        public static bool IsMutuallyPrimary(uint firstNum, uint secondNum)
        {
            uint n = firstNum < secondNum ? firstNum : secondNum;

            for (int i = 2; i < n; i++)
            {
                if ((firstNum % i == 0) && (secondNum % i == 0)) return false;
            }

            return true;
        }
    }
}
