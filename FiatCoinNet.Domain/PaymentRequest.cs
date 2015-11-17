using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiatCoinNet.Domain
{
    public class PaymentRequest
    {
        public string PayTo { get; set; }
        
        public string Memo { get; set; }

        public decimal Amount { get; set; } 
    }
}
