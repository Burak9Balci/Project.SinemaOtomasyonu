using PagedList;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class PaginationPageVM
    {
        public IPagedList<MovieVM> PagedMovies { get; set; }
    }
}