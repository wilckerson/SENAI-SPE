using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.Models
{
    public class ItemMenu
    {
        public string Titulo { get; set; }
        public string Url { get; set; }
        public List<ItemMenu> SubMenu { get; set; }
    }
}