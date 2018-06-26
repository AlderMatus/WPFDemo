using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

namespace Carefusion.Supply.CSharpResource
{
    public class SupplyResource
    {
        private static readonly ResourceManager stringTable = new ResourceManager("Carefusion.Supply.CSharpResource.CSharpResource", System.Reflection.Assembly.GetExecutingAssembly());
        public static ResourceManager GetResource()
        {
            return stringTable;
        }
    }
}
