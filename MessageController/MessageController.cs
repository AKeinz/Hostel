using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageController
{
    public class MessageController : IMessageContrroller
    {
        public bool SendMessage(string phone, string text)
        {
            return true;
        }
    }
}
