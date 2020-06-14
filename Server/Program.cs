using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        public static Dictionary<int, BaglanmisIstemci> bagli_istemciler = new Dictionary<int, BaglanmisIstemci>();
        static void Main(string[] args)
        {
            Server mServer = new Server();
            mServer.StartListen();

            Console.WriteLine("Sunucu başlatıldı");
            Console.ReadLine();
        }
    }
}
