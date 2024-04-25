using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Exceptions;
public class PassInException : System.Exception
{
    public PassInException(string message) : base(message)
    {
        
    }
}
