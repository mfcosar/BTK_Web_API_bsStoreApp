using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message): base(message)
        { //new'lenemediği için public olmasının anlamı yok. inherit eden class'lar kullansın diye protected
        }
    }
}
