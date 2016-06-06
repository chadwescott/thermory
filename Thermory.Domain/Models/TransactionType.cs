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
            {TransactionTypeNames.OrderCreate, TransactionTypes.OrderCreate},
            {TransactionTypeNames.OrderDelete, TransactionTypes.OrderDelete},
            {TransactionTypeNames.OrderEdit, TransactionTypes.OrderEdit},
            {TransactionTypeNames.OrderLoaded, TransactionTypes.OrderLoaded},
            {TransactionTypeNames.OrderPackagingSlipCreated, TransactionTypes.OrderPackagingSlipCreated},
            {TransactionTypeNames.OrderPulled, TransactionTypes.OrderPulled},
            {TransactionTypeNames.OrderReceived, TransactionTypes.OrderReceived},
            {TransactionTypeNames.OrderWarehouseReceived, TransactionTypes.OrderWarehouseReceived}
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
