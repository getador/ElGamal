using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ElGamalCriptografic.Classes;
using Microsoft.Win32;
using System.IO;

namespace ElGamalCriptografic.ViewModels
{
    class ViewWorker : INotifyPropertyChanged
    {
        static Random random = new Random();
        CriptoWorker first;
        public ViewWorker()
        {
            EncriptMessageA = "";
            EncriptMessageB = "";
            first = new CriptoWorker(1000, random);
            StatusFirst = first.ToString().Replace(" ", Environment.NewLine);
        }

        #region для шифратора
        private uint pElement;
        public uint PElement
        {
            get { return pElement; }
            set
            {
                if (value == pElement)
                    return;
                pElement = value;
                OnPropertyChanged("PElement");
            }
        }
        private uint qElement;
        public uint QElement
        {
            get { return qElement; }
            set
            {
                if (value == qElement)
                    return;
                qElement = value;
                OnPropertyChanged("QElement");
            }
        }
        private uint mElement;
        public uint MElement
        {
            get { return mElement; }
            set
            {
                if (value == mElement)
                    return;
                mElement = value;
                OnPropertyChanged("MElement");
            }
        }
        private uint dElement;
        public uint DElement
        {
            get { return dElement; }
            set
            {
                if (value == dElement)
                    return;
                dElement = value;
                OnPropertyChanged("DElement");
            }
        }
        private uint eElement;
        public uint EElement
        {
            get { return eElement; }
            set
            {
                if (value == eElement)
                    return;
                eElement = value;
                OnPropertyChanged("EElement");
            }
        }
        private uint nElement;
        public uint NElement
        {
            get { return nElement; }
            set
            {
                if (value == nElement)
                    return;
                nElement = value;
                OnPropertyChanged("NElement");
            }
        }
        #endregion

        private string statusFirst;
        public string StatusFirst
        {
            get { return statusFirst; }
            set
            {
                if (value == statusFirst)
                    return;
                statusFirst = value;
                OnPropertyChanged("StatusFirst");
            }
        }


        private string encriptMessageA;
        public string EncriptMessageA
        {
            get { return encriptMessageA; }
            set
            {
                if (value == encriptMessageA)
                    return;
                encriptMessageA = value;
                OnPropertyChanged("EncriptMessageA");
            }
        }

        private string encriptMessageB;
        public string EncriptMessageB
        {
            get { return encriptMessageB; }
            set
            {
                if (value == encriptMessageB)
                    return;
                encriptMessageB = value;
                OnPropertyChanged("EncriptMessageB");
            }
        }

        private string unEncriptMessage;
        public string UnEncriptMessage
        {
            get { return unEncriptMessage; }
            set
            {
                if (value == unEncriptMessage)
                    return;
                unEncriptMessage = value;
                OnPropertyChanged("UnEncriptMessage");
            }
        }
        private string forEncript;
        public string ForEncript
        {
            get { return forEncript; }
            set { forEncript = value; }
        }
        public ICommand EncriptBtnClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (first!=null)
                    {
                        //ChangeElementInClass();
                        first.Encript(forEncript, "alphabetRuLow");
                        EncriptMessageA = first.A;
                        EncriptMessageB = first.B;
                    }
                });
            }
        }
        public ICommand UnEncriptBtnClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (first != null)
                    {
                        //ChangeElementInClass();
                        UnEncriptMessage = $@"{first.Uncript(EncriptMessageA,EncriptMessageB,"alphabetRuLow")}";
                    }
                });
            }
        }
        public ICommand UnEncriptBtnFromFile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string a, b;
                        using (StreamReader stream = new StreamReader(openFileDialog.FileName))
                        {
                            a = stream.ReadLine();
                            b = stream.ReadLine();
                        }
                        if (a != string.Empty &&
                            b != string.Empty &&
                            a.Length == b.Length)
                            UnEncriptMessage = $@"{first.Uncript(a, b, "alphabetRuLow")}";

                        //ChangeElementInClass();
                        //StreamReader reader = new StreamReader(openFileDialog.FileName);
                        //UnEncriptMessage = $@"{first.ToUnEncript(reader.ReadLine())}";
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
