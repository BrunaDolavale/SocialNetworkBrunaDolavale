using DomainModel.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class User:EntityBase
    {
        public User()
        {
            Birth = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
        public string Gender { get; set; }
        public string Sexuality { get; set; }
        public string Description { get; set; }
        public string CellphoneNumber { get; set; }
        public string Email { get; set; }
        public string SchoolLevel { get; set; }
        public string Office { get; set; }
        public virtual PhotoProfile PhotoProfile { get; set; }
    }
}
