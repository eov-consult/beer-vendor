using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brewer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<Beer> Beers { get; set; } = null!;

    }
}
