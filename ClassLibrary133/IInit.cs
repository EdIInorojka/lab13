using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary133
{
    public interface IInit
    {
        int CompareTo<T>(T data) where T : IInit, ICloneable, new();
        void Init();
        void RandomInit();
    }
}
