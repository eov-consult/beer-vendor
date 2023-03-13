using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Classes
{
    internal class VendorManager : IVendorManager
    {
        private readonly ILogger<VendorManager> logger;
        private readonly IContext context;

        public VendorManager(ILogger<VendorManager> logger, IContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IEnumerable<Vendor> Get()
        {
            return context.Vendors.Include(a => a.VendorBeers).ThenInclude(a => a.Beer).AsNoTracking().ToList();
        }

        public Vendor? Get(int id)
        {
            return context.Vendors.Include(a => a.VendorBeers).ThenInclude(a => a.Beer).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
    }
}
