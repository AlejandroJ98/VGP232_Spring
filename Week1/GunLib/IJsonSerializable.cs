using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunLib
{
    public interface IJsonSerializable
    {
        bool LoadJson(string path);
        bool SaveAsJson(string path);
    }

}
