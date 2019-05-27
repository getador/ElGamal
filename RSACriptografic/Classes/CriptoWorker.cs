using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Numerics;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace ElGamalCriptografic.Classes
{
    class CriptoWorker
    {
        BigInteger p;
        BigInteger g;
        BigInteger x;
        BigInteger y;
        BigInteger k;
        string a;
        string b;

        public BigInteger P { get => p; set => p = value; }
        public BigInteger G { get => g; set => g = value; }
        public BigInteger X { get => x; set => x = value; }
        public BigInteger Y { get => y; set => y = value; }
        public BigInteger K { get => k; set => k = value; }
        public string A { get => a; set => a = value; }
        public string B { get => b; set => b = value; }

        public CriptoWorker(BigInteger p, BigInteger g,Random random)
        {
            this.p = p;
            this.g = g;
            GenerateElements(random);
        }
        public CriptoWorker(int maxValue,Random random)
        {
            if (maxValue<100)
                maxValue = 100;
            while (!SimpleNumberWorker.IsPrime(p))
                p = random.Next(33, maxValue);
            g = SimpleNumberWorker.FindPrimitive(p);
            GenerateElements(random);
        }

        public CriptoWorker(BigInteger p, BigInteger g, BigInteger x, BigInteger y, BigInteger k)
        {
            this.p = p;
            this.g = g;
            this.x = x;
            this.y = y;
            this.k = k;
        }

        public void Encript(string message,string alphabetName)
        {
            if (message != null)
            {
                a = string.Empty;
                b = string.Empty;
                Type t = typeof(Alphabet);
                FieldInfo field = t.GetField(alphabetName);
                if (field != null)
                {
                    string alphabet = (string)field.GetValue(null);
                    for (int i = 0; i < message.Length; i++)
                    {
                        if (alphabet.Contains(message[i]))
                        {
                            int index = alphabet.Select((x, ind) => new { element = x, index = ind }).First(y => y.element == message[i]).index;
                            a += BigInteger.ModPow(g, k, p) + " ";
                            b += BigInteger.ModPow(BigInteger.Pow(y, (int)k) * index, 1, p) + " ";
                        }
                    }
                }
                using (StreamWriter stream = new StreamWriter("Encript.txt", false))
                {
                    stream.WriteLine(a);
                    stream.WriteLine(b);
                    stream.WriteLine(ToString());
                }
            }
        }

        public string Uncript(string alphabetName)
        {
            string str = string.Empty;
            Type t = typeof(Alphabet);
            FieldInfo field = t.GetField(alphabetName);

            if (a != string.Empty && b != string.Empty && field!=null)
            {
                string alphabet = (string)field.GetValue(null);
                string[] encriptA = a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] encriptB = b.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (encriptA.Length==encriptB.Length)
                {
                    for (int i = 0, length = encriptA.Length; i < length; i++)
                    {
                        str += alphabet[(int)BigInteger.ModPow(BigInteger.Parse(encriptB[i]) * BigInteger.Pow(BigInteger.Parse(encriptA[i]), (int)(p - 1 - x)), 1, p)];
                    }
                }
            }
            return str;
        }

        public string Uncript(string a,string b,string alphabetName)
        {
            string str = string.Empty;
            Type t = typeof(Alphabet);
            FieldInfo field = t.GetField(alphabetName);

            if (a != string.Empty && b != string.Empty && field != null)
            {
                string alphabet = (string)field.GetValue(null);
                string[] encriptA = a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] encriptB = b.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (encriptA.Length == encriptB.Length)
                {
                    for (int i = 0, length = encriptA.Length; i < length; i++)
                    {
                        str += alphabet[(int)BigInteger.ModPow(BigInteger.Parse(encriptB[i]) * BigInteger.Pow(BigInteger.Parse(encriptA[i]), (int)(p - 1 - x)), 1, p)];
                    }
                }
            }
            return str;
        }

        public override string ToString()
        {
            return $"{p} {g} {x} {y} {k}";
        }

        private void GenerateElements(Random random)
        {
            SetX(random);
            SetY();
            SetK(random);
        }

        private void SetX(Random random)
        {
            x = 2;
            while (!SimpleNumberWorker.IsPrime(x))
                x = random.Next(2, (int)p - 1);
        }

        private void SetY()
        {
            y = BigInteger.ModPow(g, x, p);
        }
        private void SetK(Random random)
        {
            k = 2;
            while (!SimpleNumberWorker.IsMutuallyPrimary(k, p - 1))
                k = random.Next(2, (int)p - 1);
        }
    }
}
