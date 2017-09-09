using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almohami.Core.Helper
{
    public class SMTPGenralClass
    {
        public string SMTPServer { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPassword { get; set; }
        public bool SSL { get; set; }
    }
}
