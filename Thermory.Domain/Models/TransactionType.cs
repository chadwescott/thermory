using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;

namespace Thermory.Domain.Models
{
    public class TransactionType
    {
        private readonly Dictionary<string, TransactionTypes> _transactionTypeLookup = new Dictionary
            <string, TransactionTypes>
        {
            {TransactionTypeNames.Audit, TransactionTypes.Audit},
            {TransactionTypeNames.OrderCreate, TransactionTypes.Audit},
            {TransactionTypeNames.OrderDelete, TransactionTypes.OrderCreate},
            {TransactionTypeNames.OrderEdit, TransactionTypes.OrderEdit},
            {TransactionTypeNames.OrderReceived, TransactionTypes.OrderReceived}
        };

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public TransactionTypes TransactionTypeEnum
        {
            get { return _transactionTypeLookup[Name]; }
        }
    }
}
