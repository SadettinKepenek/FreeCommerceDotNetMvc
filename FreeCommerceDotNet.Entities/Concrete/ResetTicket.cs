using FreeCommerceDotNet.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ResetTicket :IEntity
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public Guid tokenHash { get; set; }
        public string exprationDate { get; set; }
        public bool tokenUsed { get; set; }
    }
}
