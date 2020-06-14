using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfChat.Models;
using WpfChat.ViewModels;

namespace WpfChat.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    
    public partial class ChatPage : Page
    {
        #region deneme
        
        #endregion

        ChatViewModel chat;

        public ChatPage(String _kullaniciAdi)
        {
            InitializeComponent();
            chat = new ChatViewModel(_kullaniciAdi);
            DataContext = chat;
            
            #region deneme
            
            
            //127.0.0.1 localHost un IP adresidir. Buraya ilerde bir server kurarsanız IP'sini yazmalısınız.
            //BeginConnect sunucuya ilk bağlandığımız anda çalıştırılır. 
            

            #endregion
        }

        private void ekleClick(object sender, RoutedEventArgs e)
        {
            /*
            if (sint == 0) { 
            Gonderi gon = new Gonderi();
            gon.Mesaj = "ben gonderiom";
            gon.Gonderen = "ben";
            chat.GonderiList.Add(gon);
                sint++;
            }
            else
            {
                Gonderi gon = new Gonderi();
                gon.Mesaj = "asdassaklşjfd jksajdsajşdjşsajdş sajşdjşsadjşsajşdj şsajşdsajdjsajdsajşdjşlsaş";
                gon.Gonderen = "cezmi";
                chat.GonderiList.Add(gon);
                sint--;
            }
            */
            #region deneme
            
            #endregion

        }

        private void IlkBaglanti(IAsyncResult result)
        {
            /*
            istemci.EndConnect(result);
            if (istemci.Connected == false)
            {
                return;
            }
            else
            {
                istemci.NoDelay = true;
                ag = istemci.GetStream();
                //ag.BeginRead(alinanAraBellek, 0, 4096 * 2, VeriAlindiginda, ag);
                byte[] myWriteBuffer = Encoding.UTF8.GetBytes(kullaniciAdi);
                ag.Write(myWriteBuffer, 0, myWriteBuffer.Length);

            }
            */
        }

        private void VeriAlindiginda(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }










        private void ListeDegisti(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = sender as ScrollViewer;
            bool AutoScrollToEnd = true;
            if (sv.Tag != null)
            {
                AutoScrollToEnd = (bool)sv.Tag;
            }
            if (e.ExtentHeightChange == 0)// user scroll
            {
                AutoScrollToEnd = sv.ScrollableHeight == sv.VerticalOffset;
            }
            else// content change
            {
                if (AutoScrollToEnd)
                {
                    sv.ScrollToEnd();
                }
            }
            sv.Tag = AutoScrollToEnd;
            return;
        }
    }
}
