using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponLib
{
    public interface ICsvSerializable
    {
        bool LoadCSV(string path);
        bool SaveAsCSV(string path);
    }
}
