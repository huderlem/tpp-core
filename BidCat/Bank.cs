using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPPCommon.Logging;

namespace BidCat
{
    /// <summary>
    /// Base class to handle user's 
    /// </summary>
    internal abstract class Bank
    {
        /// <summary>
        /// Delegate function to check currently-reserved money for a user.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>reserved money amount</returns>
        public delegate int ReservedMoneyChecker(string userId);

        protected HashSet<ReservedMoneyChecker> ReservedMoneyCheckers = new HashSet<ReservedMoneyChecker>();
        protected TPPLoggerBase Logger;

        public Bank(TPPLoggerBase logger)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Add a reserved money checker function, which will be used to generated a total sum of reserved money for a user.
        /// <seealso cref="RemoveReservedMoneyChecker"/>
        /// <seealso cref="GetReservedMoney"/>
        /// </summary>
        /// <param name="checkerFunc">function which gets the current reserved money for a user</param>
        public void AddReservedMoneyChecker(ReservedMoneyChecker checkerFunc)
        {
            if (!this.ReservedMoneyCheckers.Add(checkerFunc))
            {
                this.Logger.LogWarning($"Attempted to add duplicate reserved money checker to BidCat bank: {checkerFunc.ToString()}");
            }
        }

        /// <summary>
        /// Remove a reserved money checker function.
        /// <seealso cref="AddReservedMoneyChecker"/>
        /// <seealso cref="GetReservedMoney"/>
        /// </summary>
        /// <param name="checkerFunc">function which gets the current reserved money for a user</param>
        public void RemoveReservedMoneyChecker(ReservedMoneyChecker checkerFunc)
        {
            if (!this.ReservedMoneyCheckers.Remove(checkerFunc))
            {
                this.Logger.LogWarning($"Attempted to remove a reserved money checker from BidCat bank, but it wasn't present: {checkerFunc.ToString()}");
            }
        }

        /// <summary>
        /// Gets the total amount of reserved money for a user.
        /// Reserved money is money that is reserved "in-memory" and not yet committed to storage.
        /// Reserved money is tracked by the caller by registering a reserved money tracker with this Bank instance.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>reserved money amount</returns>
        public int GetReservedMoney(string userId)
        {
            return this.ReservedMoneyCheckers.Sum(func => func(userId));
        }

        /// <summary>
        /// Gets a user's total money.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>user's total money</returns>
        public int GetTotalMoney(string userId)
        {
            return this.GetStoredMoney(userId);
        }

        /// <summary>
        /// Gets the amount of money available to a user.
        /// 
        /// Available money is the total amount of money that the user has minus his reserved money. It is
        /// the amount of money currently available for use.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetAvailableMoney(string userId)
        {
            return this.GetTotalMoney(userId) - this.GetReservedMoney(userId);
        }

        protected abstract int GetStoredMoney(string userId);
        protected abstract void AdjustStoredMoney(string userId, int changeAmount);
        protected abstract void RecordTransaction(Transaction);
    }
}
