using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Salon : BaseEntity
    {
        public override string ToString()
        {
            return $"Salon {SalonNo}";
        }
        public string SalonNo { get; set; }
        public int Capacity { get; set; }
        //Relational Property
        public virtual List<Session> Seanses { get; set; }
        public virtual List<Movie> Movies { get; set; }
    }
}
