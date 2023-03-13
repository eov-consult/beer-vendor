﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float AlcoholDegree { get; set; }
        public float Amount { get; set; }
        public int BrewerId { get; set; }
        public Brewer Brewer { get; set; } = null!;
        public IEnumerable<VendorBeer> VendorBeers { get; set; } = null!;

    }
}
