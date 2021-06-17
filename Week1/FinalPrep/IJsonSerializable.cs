using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2b
{
    public interface IJsonSerializable
    {
        bool LoadJson(string path);
        bool SaveAsJson(string path);
    }

}
