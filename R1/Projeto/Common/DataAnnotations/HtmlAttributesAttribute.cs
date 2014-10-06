using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.DataAnnotations
{
    public class HtmlAttributesAttribute : Attribute
    {
        public int Size { get; set; }
        public int MaxLength { get; set; }
        public Boolean AllowOnlyNumber { get; set; }
    }
}
