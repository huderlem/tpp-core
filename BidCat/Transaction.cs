using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using TPPCommon.Models;

namespace BidCat
{
    /// <summary>
    /// Model for bank transaction objects.
    /// </summary>
    [Table(TableName)]
    public class Transaction : Model
    {
        // Table name.
        public const string TableName = "transactions";

        // Field names.
        public const string UserIdField = "user";
        public const string ChangeField = "change";
        public const string TimestampField = "timestamp";
        public const string OldBalanceField = "old_balance";
        public const string NewBalanceField = "new_balance";

        /// <summary>
        /// User id that the (chat) service, which this user originates from, provided.
        /// </summary>
        [BsonElement(UserIdField)]
        public readonly string UserId;

        /// <summary>
        /// Amount of change in money.
        /// </summary>
        [BsonElement(ChangeField)]
        public readonly int Change;

        /// <summary>
        /// Time of the transaction.
        /// </summary>
        [BsonElement(TimestampField)]
        public readonly DateTime Timestamp;

        /// <summary>
        /// Previous money balance, before the transaction.
        /// </summary>
        [BsonElement(OldBalanceField)]
        public readonly int OldBalance;

        /// <summary>
        /// New money balance, after the transaction.
        /// </summary>
        [BsonElement(NewBalanceField)]
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
