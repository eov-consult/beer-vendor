using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
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
    internal class VendorBeerManager : IVendorBeerManager
    {
        private readonly ILogger<VendorBeerManager> logger;
        private readonly IContext context;

        public VendorBeerManager(ILogger<VendorBeerManager> logger, IContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IEnumerable<VendorBeer> Get()
        {
            return context.VendorBeers.Include(a => a.Vendor).Include(a => a.Beer).AsNoTracking().ToList();
        }

        public VendorBeer? Get(int id)
        {
            return context.VendorBeers.Include(a => a.Vendor).Include(a => a.Beer).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public VendorBeer AddOrUpdateBeerVendor(int beerId, int vendorId, int quantity)
        {
            if (beerId == 0 || vendorId == 0)
                throw new ArgumentNullException();

            var beer = context.Beer.AsNoTracking().FirstOrDefault(a => a.Id == beerId);
            if (beer == null)
                throw new BeerNotExistException();

            var vendor = context.Vendors.AsNoTracking().FirstOrDefault(a => a.Id == vendorId);
            if (vendor == null)
                throw new VendorNotExistException();

            try
            {
                VendorBeer? vb;
                vb = context.VendorBeers.FirstOrDefault(a => a.BeerId == beerId && a.VendorId == vendorId);
                if (vb == null)
                {
                    vb = new VendorBeer
                    {
                        BeerId = beerId,
                        VendorId = vendorId,
                        Quantity = quantity
                    };
                    context.VendorBeers.Add(vb);
                }
                else
                    vb.Quantity = quantity;

                context.SaveChanges();

                return vb;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during AddBeerToVendor");
                throw;
            }
        }

        public ReturnedEstimateModel Estimate(EstimateDataModel data)
        {
            try
            {
                if (data == null)
                    throw new ArgumentNullException();

                ReturnedEstimateModel ret = new ReturnedEstimateModel();

                var vendor = context.Vendors.AsNoTracking().FirstOrDefault(a => a.Id == data.VendorId);
                if (vendor == null)
                    throw new VendorNotExistException();

                if (data.Beers == null || !data.Beers.Any())
                    throw new EmptyOrderException();

                if (data.Beers.Count(a => data.Beers.Any(b => b.BeerId == a.BeerId)) > 1)
                    throw new DuplicatesInCommandException();

                var vendorBeers = context.VendorBeers.Include(a => a.Vendor).Include(a => a.Beer)
                    .AsNoTracking().Where(a => a.VendorId == data.VendorId).ToList();

                if (data.Beers.Any(a => vendorBeers.Any(v => v.BeerId != a.BeerId)))
                    throw new BeerNotSellByVendorException();

                if (data.Beers.Any(a => vendorBeers.Any(v => v.BeerId == a.BeerId && v.Quantity < a.Quantity)))
                    throw new OrderGreaterThanStockException();


                var totalPrice = vendorBeers
                    .Where(a => data.Beers.Any(b => b.BeerId == a.BeerId))
                    .Sum(a => data.Beers.First(b => b.BeerId == a.BeerId).Quantity * a.Beer.Amount);
                
                if (data.Beers.Count() > 20) //Discount 20% if more than 20 beers
                    totalPrice = totalPrice - (totalPrice / 100) * 20;
                else if (data.Beers.Count() > 10) //Discount 10% if more than 10 beers
                    totalPrice = totalPrice - (totalPrice / 100) * 10;

                ret.Summary = data;
                ret.Amount = totalPrice;

                return ret;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during estimate");
                throw;
            }
        }
    }
}
