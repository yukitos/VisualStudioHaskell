using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.VisualStudioHaskell.Options
{
    interface IOptionsService
    {
        void SaveString(string name, string category, string value);
        string LoadString(string name, string category);
    }
}
