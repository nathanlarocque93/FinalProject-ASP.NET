using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfit.Models
{
    public class NonProfitDonator
    {

        [Key]
        public int donatorID { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string firstname { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string lastname { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string email { get; set; }

        [Required]
        [Column(TypeName = "bigint")]
        public long phone { get; set; }

        [ForeignKey("NonProfitLocation")]
        public int locationID { get; set; }

        public virtual NonProfitLocation NonProfitLocation { get; set; }
    }
}
