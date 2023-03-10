using System;
using System.Collections.Generic;

namespace AdAgency.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersOptionals = new HashSet<OrdersOptional>();
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CustomerId { get; set; }
        public int PlaceId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual AdPlace Place { get; set; } = null!;
        public virtual ICollection<OrdersOptional> OrdersOptionals { get; set; }
    }
}
