using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalShop.Entity
{
    public class Import
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        [ForeignKey("Admin")]
        public int CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        public Admin Admin { get; set; }
        public List<ImportDetail> ImportDetails { get; set; }
    }
}
