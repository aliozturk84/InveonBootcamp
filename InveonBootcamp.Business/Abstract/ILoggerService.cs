using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Abstract
{
    public interface ILoggerService
    {
        public void Write(string message);
    }
}
