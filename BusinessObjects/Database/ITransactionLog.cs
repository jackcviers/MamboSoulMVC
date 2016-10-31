using System;

namespace BusinessObjects.Database
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionLog
    {
        /// <summary>
        /// Public property that represents the TransactionId field.
        /// Description: 
        /// </summary>
        int? TransactionId { get; set; }
    }
}