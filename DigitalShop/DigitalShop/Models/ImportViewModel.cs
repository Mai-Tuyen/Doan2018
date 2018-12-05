using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShop.Models
{
    public class ImportViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameCreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public string Detail { get; set; }
    }
}
