using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("CustomerId")]
        public List<Address> Addresses { get; set; }

        [ForeignKey("CustomerId")]
        public List<Order> Orders { get; set; }
    }
}
