using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    static class IstemciYoneticisi
    {
        public static void YeniBaglantiOlustur(TcpClient tcpClient)
        {
            BaglanmisIstemci yeniIstemci = new BaglanmisIstemci(tcpClient);
            Server.bagliIstemcilerListesi.Add(yeniIstemci.baglantiID, yeniIstemci);
            yeniIstemci.OkumayaBasla();

        }
    }
}
