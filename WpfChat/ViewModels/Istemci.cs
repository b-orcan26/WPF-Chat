using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfChat.Models;

namespace WpfChat.ViewModels
{
    class Istemci
    {
        #region fields
        public static TcpClient istemci;
        public static NetworkStream ag;
        private static byte[] alinanAraBellek;
        public const String myIPAddress = "127.0.0.1";
        public const int myPort = 4000;
        private ChatViewModel mViewModel;
        #endregion

        #region Constructor 

        // İstemci değerleri atanir
        public Istemci(ChatViewModel mChatViewModel)
        {
            istemci = new TcpClient();
            istemci.ReceiveBufferSize = 4096 * 2;
            istemci.SendBufferSize = 4096 * 2;
            alinanAraBellek = new byte[4096 * 2];            
            mViewModel = mChatViewModel;
        }
        #endregion

        //İstemci calismaya baslar, baglanmayi dener
        public void IstemciBaslat()
        {
            IPAddress ip = IPAddress.Parse(myIPAddress);
            istemci.BeginConnect(ip, myPort, new AsyncCallback(IlkBaglanti), istemci);
        }

        //Eger baglanti basariliysa serveri dinlemeye baslar ve kullanıcı adını servera iletir
        private void IlkBaglanti(IAsyncResult result)
        {
            istemci.EndConnect(result);
            if (istemci.Connected == false)
            {
                return;
            }
            else
            {
                istemci.NoDelay = true;
                ag = istemci.GetStream();
                ag.BeginRead(alinanAraBellek, 0, 4096 * 2, VeriAlindi, ag);
                
                Messages message = new Messages()
                {
                    Key = MessageKeys.UserName,
                    Icerik = mViewModel.KullaniciAdi
                };

                string output = JsonConvert.SerializeObject(message);
                byte[] myWriteBuffer = Encoding.UTF8.GetBytes(output);
                ag.Write(myWriteBuffer, 0, myWriteBuffer.Length);

            }
        }

        // Server'dan bir veri alındığında bu metot çalışır , server ve client'lar arasındaki veri alışverişi Messages sınıfından
        //  nesne üretilerek gerçekleşir böylece iletişimde bir standart yakalanmış olur
        //  Veri alındığında Messages sınıfının bir örneğine dönüştürülür , bu nesnenin Key'ine bakılarak işlem yapılabilmesi için
        //  MesajYoneticisi fonksiyonuna iletilir ve Server'ı dinlemeye devam eder 
        private void VeriAlindi(IAsyncResult result)
        {
            try
            {
                int uzunluk = ag.EndRead(result);
                if (uzunluk <= 0)
                {
                    return;
                }

                byte[] yeniBytes = new byte[uzunluk];
                Array.Copy(alinanAraBellek, yeniBytes, uzunluk);
                String post = Encoding.UTF8.GetString(yeniBytes);
                //

                Messages message = Messages.MessageParse<Messages>(post);
                Console.WriteLine(message.Icerik);
                Console.WriteLine(message.Key);
                
                MesajYoneticisi(message);

                ag.BeginRead(alinanAraBellek, 0, 4096 * 2, VeriAlindi, ag);

            }
            catch (Exception)
            {
                IstemciKapat();
                return;
            }
        }

        // Gelen mesajın keyine bakarak ViewModel'i güncelller

        private void MesajYoneticisi(Messages message)
        {
            switch (message.Key)
            {
                case MessageKeys.ConnectedUsers:

                    try
                    {                      
                        mViewModel.BaglananlarListesi = Messages.MessageParse<ObservableCollection<String>>(message.Icerik);
                    }
                    catch (Exception e)
                    {
                        IstemciKapat();
                        Console.WriteLine("ConnectedUsers error:"+e.Message);
                    }
                    break;

                case MessageKeys.Post:

                    try
                    {                        
                        Gonderi gelenGonderi = Messages.MessageParse<Gonderi>(message.Icerik);
                        App.Current.Dispatcher.Invoke(() => mViewModel.GonderiList.Add(gelenGonderi));
                        // GonderiList.Add(gelenGonderi);
                        // İşlemler farklı thread'lerden yönetildiği için UI Thread'ine bağlı elemanları değiştirirken
                        // Dispatcher kullanılır
                    }
                    catch (Exception e)
                    {
                        IstemciKapat();
                        Console.WriteLine("Post error"+e.Message);
                    }


                    break;

                case MessageKeys.NewConnect:

                    try
                    {
                        String yeniKullanici = message.Icerik.ToString();
                        App.Current.Dispatcher.Invoke(() => mViewModel.BaglananlarListesi.Add(yeniKullanici));
                    }
                    catch (Exception e)
                    {
                        IstemciKapat();
                        Console.WriteLine("NewConnect error"+e.Message);
                    }
                    break;


                case MessageKeys.Disconnect:

                    try
                    {                        
                        String ayrilanKullanici = Messages.MessageParse<String>(message.Icerik);
                        App.Current.Dispatcher.Invoke(() => mViewModel.BaglananlarListesi.Remove(ayrilanKullanici));
                    }
                    catch (Exception e)
                    {
                        IstemciKapat();
                        Console.WriteLine("Disconnect error"+e.Message);
                    }

                    break;
            }
        }

        // Chat odasında bir mesaj gönderirken bu metot ViewModel tarafından çağrılır TextBox'taki mesaj içeriği Server'a iletilir
        public void PostGonder()
        {
            Gonderi mGonderi = new Gonderi();
            mGonderi.Gonderen = mViewModel.KullaniciAdi;
            mGonderi.Ileti = mViewModel.TextIleti;

            mViewModel.GonderiList.Add(mGonderi);


            // Servera gonder
            Messages message = new Messages()
            {
                Key = MessageKeys.Post,
                Icerik = JsonConvert.SerializeObject(mGonderi)
            };

            string output = JsonConvert.SerializeObject(message);
            byte[] myWriteBuffer = Encoding.UTF8.GetBytes(output);
            ag.Write(myWriteBuffer, 0, myWriteBuffer.Length);
        }

        public void IstemciKapat()
        {
            istemci.Close();
        }



    }
}
