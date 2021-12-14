using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfit.Models
{
    public class NonProfitLocation
    {

        [Key]
        public int locationID { get; set; }

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
        [Column(TypeName = "bigint")]
        public long phone { get; set; }

        [ForeignKey("NonProfitOwner")]
        public int OwnerID { get; set; }

        public virtual NonProfitOwner NonProfitOwner { get; set; }

    }
}
