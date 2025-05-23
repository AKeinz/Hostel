using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HostelException : Exception
    {
        public string Message;
        public HostelException(string message) { Message = message; }
    }
}
