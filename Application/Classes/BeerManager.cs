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
    internal class BeerManager : IBeerManager
    {
        private readonly IContext context;
        public readonly ILogger<BeerManager> logger;

        public BeerManager(ILogger<BeerManager> logger, IContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IEnumerable<Beer> Get()
        {
            return context.Beer.Include(a => a.Brewer).Include(a => a.VendorBeers).ThenInclude(a => a.Vendor).AsNoTracking().ToList();
        }

        public Beer? Get(int id)
        {
            return context.Beer.Include(a => a.Brewer).Include(a => a.VendorBeers).ThenInclude(a => a.Vendor).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }

        public Beer Add(CreateBeerModel data)
        {
            if (data == null)
                throw new ArgumentNullException();
            if (data.BrewerId == null || data.BrewerId == 0)
                throw new BrewerNotExistException();

            var brewer = context.Brewers.AsNoTracking().FirstOrDefault(a => a.Id == data.BrewerId);
            if (brewer == null)
                throw new BrewerNotExistException();

            try
            {
                Beer createdBeer = new Beer
                {
                    Name = data.Name,
                    AlcoholDegree = data.AlcoholDegree,
                    Amount = data.Amount,
                    BrewerId = brewer.Id
                };

                context.Beer.Add(createdBeer);

                context.SaveChanges();

                return createdBeer;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during Beer creation");
                throw;
            }
        }

        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            try
            {
                var beer = context.Beer.FirstOrDefault(a => a.Id == id);
                if (beer == null)
                    throw new BeerNotExistException();

                context.Beer.Remove(beer);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error during delete beer {id}");
                throw;
            }
        }
    }
}
