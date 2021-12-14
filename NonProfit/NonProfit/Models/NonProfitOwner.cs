using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfit.Models
{
    public class NonProfitOwner
    {
        [Key]
        public int ownerID { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string firstname { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string lastname { get; set; }

        [Required]
        [Column(TypeName = "bigint")]
        public long phone { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string email { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string city { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string street { get; set; }

        [Required]
        [Column(TypeName = "varchar(6)")]
        public string postalcode { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public double salary { get; set; }
    }
}
