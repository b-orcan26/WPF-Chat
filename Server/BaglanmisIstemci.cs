using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfChat.Models;

namespace Server
{
    class BaglanmisIstemci
    {
        public String kullaniciAdi;
        public TcpClient socket;
        public NetworkStream ag;
        public byte[] alinanArabellek;
        public int baglantiID;


        public BaglanmisIstemci(TcpClient tcpClient)
        {
            socket = tcpClient;
            baglantiID = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;

            socket.ReceiveBufferSize = 4096 * 2;
            socket.SendBufferSize = 4096 * 2;
            alinanArabellek = new byte[4096 * 2];
            ag = socket.GetStream();
            Console.WriteLine("BaglantiID'si {0} Olan Kullanıcı Sunucuya Baglandi", baglantiID);
        }


        public void OkumayaBasla()
        {
            socket.ReceiveBufferSize = 4096 * 2;

            socket.SendBufferSize = 4096 * 2;

            alinanArabellek = new byte[4096 * 2];

            ag.BeginRead(alinanArabellek, 0, socket.ReceiveBufferSize, new AsyncCallback(Oku), ag);
            
        }

        public void Oku(IAsyncResult result)
        {
            try
            {
                int uzunluk = ag.EndRead(result);
                if (uzunluk <= 0)
                {
                    BaglantiyiKapat();
                    return;
                }

                byte[] yeniBytes = new byte[uzunluk];
                Array.Copy(alinanArabellek, yeniBytes, uzunluk);

                String gelenMesaj = Encoding.UTF8.GetString(yeniBytes);

                //MESSAGE PARSE İŞLEMİ
                Console.WriteLine(gelenMesaj);
                //Messages message = JsonConvert.DeserializeObject<Messages>(gelenMesaj);
                Messages message = Messages.MessageParse<Messages>(gelenMesaj);

                MesajYoneticisi(message);

                //Aşağıdaki BeginRead methodu istemcinin akışını okur ve bir sonraki veri geldiğinde yine oku metodu çalışır.
                ag.BeginRead(alinanArabellek, 0, socket.ReceiveBufferSize, Oku, ag);

            }
            catch (Exception e)
            {
                Console.WriteLine(kullaniciAdi + " chat'ten ayrıldı ");
                BaglantiyiKapat();
                return;

            }
        }

        private void MesajYoneticisi(Messages message)
        {
            switch (message.Key)
            {
                #region kullanıcı adı
                case MessageKeys.UserName:

                    this.kullaniciAdi = message.Icerik;
                    List<String> baglananlar = Server.bagliIstemcilerListesi.Select(x => x.Value.kullaniciAdi).ToList();
                    Messages mMessage = new Messages()
                    {
                        Key = MessageKeys.ConnectedUsers,
                        Icerik = JsonConvert.SerializeObject(baglananlar)
                    };

                    //istemci ilk baglandiginda kullanıcı adını gönderir bizde buna orada chatte bulunan kullanıcıların
                    // isimlerini gönderiyoruz
                    string usernameOutput = JsonConvert.SerializeObject(mMessage);
                    byte[] myWriteBuffer = Encoding.UTF8.GetBytes(usernameOutput);
                    ag.Write(myWriteBuffer, 0, myWriteBuffer.Length);

                    //bu istemci yeni baglandigi icin sadece bunun kullanıcı adi diger bagli istemcilere gonderilecek

                    Messages messageYeniBaglanan = new Messages()
                    {
                        Key = MessageKeys.NewConnect,
                        Icerik = this.kullaniciAdi
                    };

                    string outputYeniBaglanan = JsonConvert.SerializeObject(messageYeniBaglanan);
                    myWriteBuffer = Encoding.UTF8.GetBytes(outputYeniBaglanan);


                    if (Server.bagliIstemcilerListesi.Count > 1)
                    {

                        foreach (var item in Server.bagliIstemcilerListesi)
                        {
                            if (item.Value.kullaniciAdi.Equals(this.kullaniciAdi))
                            {
                                continue;
                            }
                            else
                            {
                                item.Value.ag.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                            }
                        }

                    }



                    break;
                #endregion

                #region Post
                case MessageKeys.Post:
                    //kendisi haric diger istemcilere mesaji iletir

                    try
                    {
                        Gonderi gonderi = Messages.MessageParse<Gonderi>(message.Icerik);
                        Messages mMessage1 = new Messages()
                        {
                            Key = MessageKeys.Post,
                            Icerik = JsonConvert.SerializeObject(gonderi)
                        };

                        string gonderiOutput = JsonConvert.SerializeObject(mMessage1);
                        byte[] myWriteBuffer1 = Encoding.UTF8.GetBytes(gonderiOutput);

                        if (Server.bagliIstemcilerListesi.Count > 1)
                        {

                            foreach (var item in Server.bagliIstemcilerListesi)
                            {
                                if (item.Value.kullaniciAdi.Equals(this.kullaniciAdi))
                                {
                                    continue;
                                }
                                else
                                {
                                    item.Value.ag.Write(myWriteBuffer1, 0, myWriteBuffer1.Length);
                                }
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                    #endregion
            }
        }

        private void BaglantiyiKapat()
        {
            Server.IstemciAyrildi(baglantiID, kullaniciAdi);
            socket.Close();

            Messages messageAyrilan = new Messages()
            {
                Key = MessageKeys.Disconnect,
                Icerik = JsonConvert.SerializeObject(kullaniciAdi)
            };

            BilgiGonder(messageAyrilan);
        }

        private void BilgiGonder(Messages _message)
        {
            string ayrilanOutput = JsonConvert.SerializeObject(_message);
            byte[] myWriteBuffer = Encoding.UTF8.GetBytes(ayrilanOutput);

            if (Server.bagliIstemcilerListesi.Count > 0)
            {
                foreach (var item in Server.bagliIstemcilerListesi)
                {
                    item.Value.ag.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                }
            }
        }


    }
}
