using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBeerManager
    {
        Beer Add(CreateBeerModel data);
        void Delete(int id);
        IEnumerable<Beer> Get();
        Beer? Get(int id);
    }
}
