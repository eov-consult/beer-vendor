using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContext
    {
        DbSet<Beer> Beer { get; }
        DbSet<Brewer> Brewers { get; }
        DbSet<Vendor> Vendors { get; }
        DbSet<VendorBeer> VendorBeers { get; }

        int SaveChanges();

        EntityEntry Entry(object entity);
    }
}
