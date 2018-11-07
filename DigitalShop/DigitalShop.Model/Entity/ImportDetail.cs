using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalShop.Model.Entity
{
    public class ImportDetail
    {
        public int Id { get; set; }
        [ForeignKey("Import")]
        public int ImportId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Import Import { get; set; }
        public Product Product { get; set; }
    }
}
