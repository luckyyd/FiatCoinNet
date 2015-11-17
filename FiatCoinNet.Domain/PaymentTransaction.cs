using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FiatCoinNet.Domain
{
    [DataContract]
    public class PaymentTransaction
    {
        #region Required Fields
        [DataMember]
        public List<decimal> Amount { get; set; }

        [DataMember]
        public string CurrencyCode { get; set; }

        [DataMember]
        public List<string> scriptSig { get; set; }

        [DataMember]
        public List<string> scriptSigPubkey { get; set; }

        [DataMember]
        public int In_counter { get; set; }

        [DataMember]
        public List<string> Source { get; set; }

        [DataMember]
        public List<string> scriptPubKey { get; set; }

        [DataMember]
        public int Out_counter { get; set; }

        [DataMember]
        public List<string> Dest { get; set; }

        [DataMember]
        public int IssuerId { get; set; }

        [DataMember]
        public List<string> PreviousTransactionHash { get; set; }
        #endregion

        #region Optional Fields
        [DataMember]
        public string InvoiceID { get; set; }

        [DataMember]
        public string RefPaymentTrxId { get; set; }

        [DataMember]
        public string MemoData { get; set; }
        #endregion

        #region Framework Fields
        [IgnoreDataMember]
        public string TransactionId { get; set; }
        #endregion

        public string ToStr()
        {
            string str = "";
            foreach (var input in Source)
            {
                str += input;
            }
            foreach (var output in Dest)
            {
                str += output;
            }
            return str;
        }
    }
}
