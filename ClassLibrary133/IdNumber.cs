using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary133
{
    public interface IIdNumber
    {
        bool Equals(object obj);
        string ToString();
    }
}
