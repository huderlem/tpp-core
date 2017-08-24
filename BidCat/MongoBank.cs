using System;
using System.Collections.Generic;
using System.Text;
using TPPCommon.Logging;

namespace BidCat
{
    internal class MongoBank : Bank
    {
        public MongoBank(TPPLoggerBase logger) : base(logger)
        {
        }
    }
}
