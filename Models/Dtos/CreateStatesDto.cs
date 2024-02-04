using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class CreateStatesDto
    {
        public string Name { get; set; }
        public double Population { get; set; }
        public int CountryId { get; set; }
    }
}