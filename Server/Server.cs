using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static Dictionary<int, BaglanmisIstemci> bagliIstemcilerListesi = new Dictionary<int, BaglanmisIstemci>();
        public const String myIPAddress = "127.0.0.1";
        public const int myPort = 4000;
        public TcpListener mTcpServer;

        //Dinlemeye Basla
        public void StartListen()
        {
            IPAddress ip = IPAddress.Parse(myIPAddress);
            mTcpServer = new TcpListener(ip, myPort);

            Console.WriteLine("Server Başlatılıyor");
            try
            {
                mTcpServer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Server başlatılamadı");
                Console.WriteLine("Hata :" + e.Message);
            }

            //istemcilerin bağlanmasını bekliyoruz istemci bağlandığında IstemciBaglandi metodu çalışacak
            mTcpServer.BeginAcceptTcpClient(new AsyncCallback(IstemciBaglandi), null);

        }

        private void IstemciBaglandi(IAsyncResult result)
        {

            //istek yapan istemcinin tcp client referansını elde edelim
            TcpClient tcp_istemci = mTcpServer.EndAcceptTcpClient(result);

            //birden fazla istemcinin bağlanabilmesi için dinlemeye devam edelim
            mTcpServer.BeginAcceptTcpClient(new AsyncCallback(IstemciBaglandi), null);

            //istemcileri yöneteceğimiz sınıfa tcp client'i parametre olarak gönderelim
            IstemciYoneticisi.YeniBaglantiOlustur(tcp_istemci);
        }

        public static void IstemciAyrildi(int baglantiID, String kullaniciAdi)
        {
            bagliIstemcilerListesi.Remove(baglantiID);
            Console.WriteLine(kullaniciAdi + "chat'ten ayrildi");
        }

    }
}
