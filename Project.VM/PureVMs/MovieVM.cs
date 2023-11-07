using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.PureVMs
{
    public class MovieVM
    {
        public int ID { get; set; }
        public string FilmTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime TakedownDate { get; set; }
        public string ImagePath { get; set; }
        public string SessionTime { get; set; }
        public string SalonNo { get; set; }
    }
}
