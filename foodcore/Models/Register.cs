﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace foodcore.Models
{
    [Table("register")]
    public partial class Register
    {
        public Register()
        {
            Mycarts = new HashSet<Mycart>();
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("username")]
        [StringLength(20)]
        [Unicode(false)]
        public string Username { get; set; }
        [Column("pwd")]
        [StringLength(20)]
        [Unicode(false)]
        public string Pwd { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column("email")]
        [StringLength(20)]
        [Unicode(false)]
        public string Email { get; set; }
        [Column("gender")]
        public bool? Gender { get; set; }
        [Column("phone")]
        [StringLength(20)]
        [Unicode(false)]
        public string Phone { get; set; }
        [Column("nationality")]
        [StringLength(20)]
        [Unicode(false)]
        public string Nationality { get; set; }

        [InverseProperty(nameof(Mycart.UsernameNavigation))]
        public virtual ICollection<Mycart> Mycarts { get; set; }
        [InverseProperty(nameof(Order.UsernameNavigation))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}