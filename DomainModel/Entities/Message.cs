using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Message : EntityBase
    {
        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
        public DateTime DateTimeSent { get; set; }
        public string Content { get; set; }
    }

}
