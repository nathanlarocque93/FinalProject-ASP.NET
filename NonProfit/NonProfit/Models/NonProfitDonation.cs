using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfit.Models
{
    [Authorize(Roles="superadmin")]
    public class NonProfitDonation
    {
        [Key]
        public int donationID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public double amount { get; set; }

        [Required]
        [Column(TypeName = "bigint")]
        public long creditcard { get; set; }

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
        [Column(TypeName = "date")]
        public DateTime date { get; set;}

        [ForeignKey("NonProfitDonator")]
        public int donatorID { get; set; }


        public virtual NonProfitDonator NonProfitDonator { get; set; }
    }
}
