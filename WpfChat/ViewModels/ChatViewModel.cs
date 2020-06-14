using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfChat.Models;

namespace WpfChat.ViewModels
{
    class ChatViewModel : ObservableObject
    {
        public static String kullaniciAdi;
        private ObservableCollection<Gonderi> gonderiList;
        private ObservableCollection<String> baglananlarListesi;
        private String textIleti;
        private Istemci istemci;

        #region Properties
        public ObservableCollection<Gonderi> GonderiList {
            get
            {
                return gonderiList;
            }
            set
            {
                gonderiList = value;
                OnPropertyChanged("GonderiList");
            }
        }

        public ObservableCollection<String> BaglananlarListesi
        {
            get
            {
                return baglananlarListesi;
            }
            set
            {
                baglananlarListesi = value;
                OnPropertyChanged("BaglananlarListesi");
            }
        }

        public String KullaniciAdi {
            get
            {
                return kullaniciAdi;
            }
            set
            {
                kullaniciAdi = value;
            }
        }

        public String TextIleti
        {
            get
            {
                return textIleti;
            }
            set
            {
                textIleti = value;
                OnPropertyChanged("TextIleti");
            }
        }

        #endregion

        #region Constructor
        public ChatViewModel(String kullaniciAdi)
        {
            GonderiList = new ObservableCollection<Gonderi>();
            KullaniciAdi = kullaniciAdi;
            CreateGonderCommand();
            istemci = new Istemci(this);
            istemci.IstemciBaslat();
        }
        #endregion

        #region GonderCommand
        public ICommand GonderCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteGonderCommand()
        {
            return !string.IsNullOrEmpty(TextIleti);
        }

        private void CreateGonderCommand()
        {
            GonderCommand = new RelayCommand(GonderExecute, null);
        }

        public void GonderExecute()
        {
            istemci.PostGonder();
            TextIleti = String.Empty;
        }
        #endregion
















    }
}
