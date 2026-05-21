using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroTripProject.Domain.Entities
{
    public class TransferMoney : BaseEntity
    {
        public string CardNumber { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransferDate { get; set; }
        public string StatusString { get; set; }
    }
}
