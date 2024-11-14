using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace analyzer
{
    public class ServiceResults
    {
        //public int Id { get; set; }
        public int PatientId { get; set; } = 0;
        public int Code { get; set; } = 0;
        public string Result { get; set; } = string.Empty;
        public int Progress { get; set; } = 0; 
    }
}
