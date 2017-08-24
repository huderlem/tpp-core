using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using TPPCommon.Models;

namespace BidCat
{
    /// <summary>
    /// Model for bank transaction objects.
    /// </summary>
    [Table("transactions")]
    public class Transaction : Model
    {
        /// <summary>
        /// User id that the (chat) service, which this user originates from, provided.
        /// </summary>
        [BsonElement("user")]
        public readonly string UserId;

        /// <summary>
        /// Amount of change in money.
        /// </summary>
        [BsonElement("change")]
        public readonly int Change;

        /// <summary>
        /// Time of the transaction.
        /// </summary>
        [BsonElement("timestamp")]
        public readonly DateTime Timestamp;

        /// <summary>
        /// Previous money balance, before the transaction.
        /// </summary>
        [BsonElement("old_balance")]
        public readonly int OldBalance;

        /// <summary>
        /// New money balance, after the transaction.
        /// </summary>
        [BsonElement("new_balance")]
        public readonly int NewBalance;

        [BsonConstructor]
        public Transaction(string userId, int change, DateTime timestamp, int oldBalance, int newBalance)
        {
            this.UserId = userId;
            this.Change = change;
            this.Timestamp = timestamp;
            this.OldBalance = oldBalance;
            this.NewBalance = newBalance;
        }
    }
}
