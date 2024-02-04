using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class UpdateStatesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Population { get; set; }
        public int CountryId { get; set; }
    }
}