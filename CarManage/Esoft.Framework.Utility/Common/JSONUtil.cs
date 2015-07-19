using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace Esoft.Framework.Utility.Common
{
    public class JSONUtil
    {
        public static bool HasValue(JToken jToken)
        {
            //return jToken.HasValues;
            return jToken != null && !string.IsNullOrEmpty(jToken.ToString());
        }
    }
}
