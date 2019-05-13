using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Numerics;
using System.Diagnostics.Contracts;

namespace ElGamalCriptografic.Classes
{
    class CriptoWorker
    {
        uint p;
        uint q;
        uint n;
        uint m;
        uint d;
        uint e;

        public CriptoWorker(int maxValue,Random random)
        {
            if (maxValue<2)
            {
                maxValue = 3;
            }
            p = 0;
            q = 0;
            while (!SimpleNumberWorker.IsPrime(p))
            {
                p = (uint)random.Next(3,maxValue);
            }
            while (!SimpleNumberWorker.IsPrime(q))
            {
                q = (uint)random.Next(3, maxValue);
            }
            n = p * q;
            m = (p - 1) * (q - 1);

            d = (uint)CalculateD((int)m);
            //d = m;
            //while (!SimpleNumberWorker.IsMutuallyPrimary(d,m))
            //{
            //    d = (uint)random.Next((int)m-1);
            //}
            e = (uint)CalculateE((int)d, (int)m);
        }
        private int CalculateD(int m)
        {
            int d = m - 1;
            for (int i = 2; i <= m; i++)
            {
                if ((m % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 1;
                }
            }
            return d;
        }
        private int CalculateE(int d, int m)
        {
            int e = 10;
            while ((e * d % m) != 1)
            {
                e++;
            }
            return e;
        }
        public CriptoWorker(uint p, uint q, uint n, uint m, uint d, uint e)
        {
            this.p = p;
            this.q = q;
            this.n = n;
            this.m = m;
            this.d = d;
            this.e = e;
        }
        /// <summary>
        /// Шифрование RSA
        /// </summary>
        /// <param name="message">Сообщение для зашифровки</param>
        /// <param name="e">Первый элемент открытого ключа</param>
        /// <param name="n">Второй элемент открытого ключа</param>
        /// <returns>Зашифрованое сообщение</returns>
        public string Encript(string message)
        {
            message = message.ToLower();
            string encriptMessage = "";
            BigInteger biN = new BigInteger(n);
            for (int i = 0; i < message.Length; i++)
            {
                int index = Array.IndexOf(Alphabet.alphabet.ToCharArray(), message[i]);
                if (index <= n&&index!=-1)
                {
                    BigInteger bi = new BigInteger(index);
                    bi = BigInteger.Pow(bi, (int)e);
                    bi %= biN;
                    encriptMessage += bi.ToString() + "|";
                }
            }
            //for (int i = 0; i < message.Length; i++)
            //{
            //    int index = 0;
            //    for (int j = 0; j < Alphabet.alphabet.Length; j++)
            //    {
            //        if (message[i]==Alphabet.alphabet[j])
            //        {
            //            index = j;
            //            qwer.Add(index);
            //            break;
            //        }
            //    }
            //    if (index<n)
            //    {
            //        encriptMessage += Convert.ToString((int)(Math.Pow(index, e) % n)) + "|";
            //    }
            //    else
            //    {
            //        encriptMessage += " ";
            //    }
            //}
            using (StreamWriter stream = new StreamWriter("1.txt", false))
            {
                stream.WriteLine(encriptMessage);
                stream.WriteLine(ToString());
            }
            return encriptMessage;
        }
        public List<int> qwer = new List<int>();
        public string ToUnEncript(string message)
        {
            string unEncriptMessage = "";
            string[] arrayElement = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            BigInteger biN = new BigInteger(n);
            for (int i = 0; i < arrayElement.Length; i++)
            {
                int num = Convert.ToInt32(arrayElement[i]);
                BigInteger bi = new BigInteger(num);
                bi = BigInteger.Pow(bi, (int)d);
                bi %= biN;
                int index = Convert.ToInt32(bi.ToString());
                try
                {
                    unEncriptMessage += Alphabet.alphabet[index].ToString();
                }
                catch (Exception)
                {

                }
            }
            //return unEncriptMessage;
            //for (int i = 0; i < arrayElement.Length; i++)
            //{
            //    //try
            //    //{
            //    //unEncriptMessage += Convert.ToString((int)(Math.Pow(int.Parse(arrayElement[i]), d) % n)) + "|";

            //    unEncriptMessage += Convert.ToString(Alphabet.alphabet[(int)(Math.Pow(int.Parse(arrayElement[i]), d) % n)]);
            //    //}
            //    //catch (Exception)
            //    //{
            //    //}                
            //}
            using (StreamWriter stream = new StreamWriter("2.txt",false))
            {
                stream.WriteLine(unEncriptMessage);
                stream.WriteLine(ToString());
            }
            return unEncriptMessage;
        }
        public uint FindEelement(Random random)
        {
            uint element = 10;
            //for (int i = 2; i < m; i++)
            //{
            //    if (i*d==1%m)
            //    {
            //        element = (uint)i;
            //        break;
            //    }
            //}
            while ((element * d % m) != 1)
            {
                element++;// = (uint)random.Next((int)m - 1);
            }
            //for (int i = 0; i < m; i++)
            //{
            //    if ((i*d)%m==1)
            //    {
            //        element = (uint)i;
            //        break;
            //    }
            //}
            return element;
        }
        public override string ToString()
        {
            return $"P={p} Q={Q} M={M} D={D} E={E} N={N}";
        }
        public uint P { get => p; set => p = value; }
        public uint Q { get => q; set => q = value; }
        public uint M { get => m; set => m = value; }
        public uint D { get => d; set => d = value; }
        public uint E { get => e; set => e = value; }
        public uint N { get => n; set => n = value; }
    }
}
