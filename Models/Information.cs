using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Models
{
    public partial class Information
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Content { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? InformationName { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Information")]
        public virtual User? User { get; set; }
    }
}
