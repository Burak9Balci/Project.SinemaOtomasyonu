using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class ListMoviePageVM
    {
        public List<MovieVM> Movies { get; set; }
        public List<SessionVM> Sessions { get; set; }
    }
}