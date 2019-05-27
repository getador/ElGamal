using ElGamalCriptografic.Classes;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace ElGamalCriptografic.ViewModels
{
    internal class ViewWorker : INotifyPropertyChanged
    {
        private static readonly Random random = new Random();
        private readonly CriptoWorker first;
        public ViewWorker()
        {
            EncriptMessageA = "";
            EncriptMessageB = "";
            Type t = typeof(Alphabet);
            FieldInfo[] fields = t.GetFields();
            AlphabetItem = new ObservableCollection<string>(fields.Select(x => x.Name).ToArray());
            first = new CriptoWorker(1000, random);
            StatusFirst = first.ToString().Replace(" ", Environment.NewLine);
        }

        #region для шифратора
        private uint pElement;
        public uint PElement
        {
            get => pElement;
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
            get => qElement;
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
            get => mElement;
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
            get => dElement;
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
            get => eElement;
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
            get => nElement;
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
            get => statusFirst;
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
            get => encriptMessageA;
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
            get => encriptMessageB;
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
            get => unEncriptMessage;
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
            get => forEncript;
            set => forEncript = value;
        }

        private ObservableCollection<string> alphabetItem;
        public ObservableCollection<string> AlphabetItem
        {
            get => alphabetItem;
            set
            {
                alphabetItem = value;
                OnPropertyChanged("AlphabetItem");
            }
        }

        private string selectedAlphabetItem;
        public string SelectedAlphabetItem
        {
            get => selectedAlphabetItem;
            set
            {
                selectedAlphabetItem = value;
                OnPropertyChanged("SelectedAlphabetItem");
            }
        }

        public ICommand EncriptBtnClick => new DelegateCommand((obj) =>
                                                         {
                                                             if (first != null)
                                                             {
                                                                 //ChangeElementInClass();
                                                                 first.Encript(forEncript, SelectedAlphabetItem);
                                                                 EncriptMessageA = first.A;
                                                                 EncriptMessageB = first.B;
                                                             }
                                                         });
        public ICommand UnEncriptBtnClick => new DelegateCommand((obj) =>
                                                           {
                                                               if (first != null)
                                                               {
                                                                   //ChangeElementInClass();
                                                                   UnEncriptMessage = $@"{first.Uncript(EncriptMessageA, EncriptMessageB, SelectedAlphabetItem)}";
                                                               }
                                                           });
        public ICommand UnEncriptBtnFromFile => new DelegateCommand((obj) =>
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
                                                                          UnEncriptMessage = $@"{first.Uncript(a, b, SelectedAlphabetItem)}";

                                                                      //ChangeElementInClass();
                                                                      //StreamReader reader = new StreamReader(openFileDialog.FileName);
                                                                      //UnEncriptMessage = $@"{first.ToUnEncript(reader.ReadLine())}";
                                                                  }
                                                              });

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
