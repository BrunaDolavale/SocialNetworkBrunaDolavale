using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Conversation : EntityBase
    {
        public DateTime LastUpdate { get; set; }
        public User FirstToLike { get; set; }
        public User SecondToLike { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
