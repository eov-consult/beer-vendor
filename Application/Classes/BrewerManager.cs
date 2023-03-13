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
    internal class BrewerManager : IBrewerManager
    {
        private readonly ILogger<BrewerManager> logger;
        private readonly IContext context;

        public BrewerManager(ILogger<BrewerManager> logger, IContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IEnumerable<Brewer> Get()
        {
            return context.Brewers.Include(a => a.Beers).AsNoTracking().ToList();
        }

        public Brewer? Get(int id)
        {
            return context.Brewers.Include(a => a.Beers).AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
    }
}
