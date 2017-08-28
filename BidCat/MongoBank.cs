using System;
using System.Collections.Generic;
using System.Text;
using TPPCommon.Logging;
using TPPCommon.Models;
using TPPCommon.Persistence;

namespace BidCat
{
    internal class MongoBank : Bank
    {
        public MongoBank(TPPLoggerBase logger, IPersistence persistence) : base(logger, persistence)
        {
        }

        protected override int AdjustStoredMoney(string userId, int changeAmount)
        {
            User updatedUser = this.Persistence.FindOneAndModify(
                (User user) => string.Equals(user.Id, userId),
                string.Format("{ $inc: { {0}: {1} } }", User.MoneyField, changeAmount));

            if (updatedUser == null)
            {
                this.Logger.LogError($"Failed to adjust stored money because user was not found: {userId}");
                return 0;
            }

            return updatedUser.Money;
        }

        protected override int GetStoredMoney(string userId)
        {
            throw new NotImplementedException();
        }

        protected override void RecordTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
