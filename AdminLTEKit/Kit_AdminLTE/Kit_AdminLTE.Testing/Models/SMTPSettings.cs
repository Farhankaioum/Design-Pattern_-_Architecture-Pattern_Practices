using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kit_AdminLTE.Testing.Models
{
    public class SMTPSettings
    {
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
}
