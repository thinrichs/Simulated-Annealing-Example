using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FiveCardMatrix.interfaces
{
    public interface IStatePermuter
    {
        void PermuteStateOf<T>(IList<T> data) where T : class;
    }
}
