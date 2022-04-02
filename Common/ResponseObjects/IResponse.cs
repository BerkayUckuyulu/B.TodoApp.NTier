using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResponseObjects
{
    public interface IResponse
    {
        ResponseType ResponseType { get; set; }
    }
}
