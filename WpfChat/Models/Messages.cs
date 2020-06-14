using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChat.Models
{
    public class Messages
    {
        public String Key { get; set; }
        public String Icerik { get; set; }

        public static T MessageParse<T>(String result)
        {
            T obj = JsonConvert.DeserializeObject<T>(result);
            return obj;
        }


    }
}
