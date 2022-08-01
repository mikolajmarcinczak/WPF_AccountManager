using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Models
{
    public partial class User
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            Information = new HashSet<Information>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Password { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? UserName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Surname { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string? IsPaid { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Bill> Bills { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Information> Information { get; set; }
    }
}
