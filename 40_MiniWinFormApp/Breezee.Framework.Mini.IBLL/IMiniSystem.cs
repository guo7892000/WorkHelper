using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Breezee.Framework.Mini.Entity;


namespace Breezee.Framework.Mini.IBLL
{
    public abstract class IMiniSystem : IMiniModule
    {
        public abstract IDictionary<string, object> InitDB(IDictionary<string, string> dicQuery);
    }

    
}
