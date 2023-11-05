using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Movie : BaseEntity
    {
        public Movie()
        {
            Seanses = new List<Session>();
        }
        public override string ToString()
        {
            return FilmTitle;
        }
        public string FilmTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime TakedownDate { get; set; }
        public string ImagePath { get; set; }
        public int? SalonID { get; set; }
        //Relational Property

        public virtual List<Session> Seanses { get; set; }
        public virtual Salon Salon { get; set; }
    }
}
