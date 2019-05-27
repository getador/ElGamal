using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ElGamalCriptografic.Classes
{
    class SimpleNumberWorker
    {
        public static bool IsPrime(BigInteger num)
        {
            if (num == null) return false;
            if (num == 2) return true;
            if (num % 2 == 0) return false;

            for (BigInteger i = 3; i <= num / 2; i += 2)
            {
                if (num % i == 0) return false;
            }

            return true;
        }

        public static bool IsMutuallyPrimary(BigInteger firstNum, BigInteger secondNum)
        {
            if (firstNum == null || secondNum == null) return false;
            BigInteger n = firstNum < secondNum ? firstNum : secondNum;

            for (BigInteger i = 2; i < n; i++)
            {
                if ((firstNum % i == 0) && (secondNum % i == 0)) return false;
            }

            return true;
        }

        public static BigInteger FindPrimitive(BigInteger P)
        {
            BigInteger primitive = 2;
            for (BigInteger i = 2; i < P-1; i++)
            {
                if (IsPrimitiveRoot(i,P))
                {
                    primitive = i;
                    break;
                }
            }
            return primitive;
        }

        private static bool IsPrimitiveRoot(BigInteger el, BigInteger P)
        {
            BigInteger sum = 0;
            for (int i = 0; i < P-1; i++)
            {
                sum += BigInteger.ModPow(el, i, P);
            }
            if (sum == el * P)
                return true;
            return false;
        }
    }
}
