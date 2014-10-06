using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Entity
{
    public class EntityBaseViewModel
    {
        public int Id { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<int> UpdateUserId { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }
}
