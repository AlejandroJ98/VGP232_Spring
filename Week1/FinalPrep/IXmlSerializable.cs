using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2b
{
    public interface IXmlSerializable
    {
        bool LoadXML(string path);
        bool SaveAsXML(string path);
    }
}
