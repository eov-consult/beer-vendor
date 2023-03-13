using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VendorBeer
    {
        public int Id { get; set; }
        public int BeerId { get; set; }
        public int VendorId { get; set; }
        public int Quantity { get; set; }

        public Beer Beer { get; set; } = null!;
        public Vendor Vendor { get; set; } = null!;
    }
}
