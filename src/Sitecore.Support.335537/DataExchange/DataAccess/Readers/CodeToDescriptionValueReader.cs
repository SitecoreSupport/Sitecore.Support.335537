using Sitecore.DataExchange.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Support.DataExchange.DataAccess.Readers
{
    public class CodeToDescriptionValueReader : IValueReader
    {
        public CodeToDescriptionValueReader()
        {
            this.Values = new Dictionary<int, string>();
        }
        public IDictionary<int, string> Values { get; private set; }
        public virtual ReadResult Read(object source, DataAccessContext context)
        {
            if (source as Microsoft.Xrm.Sdk.OptionSetValue != null)
            {
                var key = ((Microsoft.Xrm.Sdk.OptionSetValue)source).Value;
                if (!this.Values.ContainsKey(key))
                {
                    return ReadResult.PositiveResult(null, DateTime.Now);
                }
                return ReadResult.PositiveResult(this.Values[key], DateTime.Now);
            }
            return ReadResult.NegativeResult(DateTime.Now);
        }
    }
}