using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageController
{
    public interface IMessageContrroller
    {
        bool SendMessage(string phone, string text);
    }
}
