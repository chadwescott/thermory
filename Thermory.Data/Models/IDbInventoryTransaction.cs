using System;
using System.ComponentModel.DataAnnotations;

namespace Thermory.Data.Models
{
    internal interface IDbInventoryTransaction
    {
        [Key]
        Guid Id { get; set; }

        int UserId { get; set; }
        DateTime CreatedOn { get; set; }
    }
}