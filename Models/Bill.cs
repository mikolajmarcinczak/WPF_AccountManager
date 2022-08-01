using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Models
{
    public partial class Bill
    {
        [Key]
        public int Id { get; set; }
        public double? January { get; set; }
        public double? February { get; set; }
        public double? March { get; set; }
        public double? April { get; set; }
        public double? May { get; set; }
        public double? June { get; set; }
        public double? July { get; set; }
        public double? August { get; set; }
        public double? September { get; set; }
        public double? October { get; set; }
        public double? November { get; set; }
        public double? December { get; set; }
        public double? BillsYear { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? BillName { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Bills")]
        public virtual User? User { get; set; }
    }
}
