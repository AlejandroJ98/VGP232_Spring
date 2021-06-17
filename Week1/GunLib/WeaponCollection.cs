using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GunLib
{
    public class WeaponCollection : List<Weapon>, IPeristence, IXmlSerializable, ICsvSerializable, IJsonSerializable
    {
        public List<Weapon> weapons { get; set; }
        public bool Load(string filename)
        {
            this.Clear();
            string extension = Path.GetExtension(filename);

            try
            {
                if (extension == ".xml")
                {
                    return LoadXML(filename);
                }
                else if (extension == ".csv")
                {
                    return LoadCSV(filename);
                }
                else if (extension == ".json")
                {
                    return LoadJson(filename);
                }
            }
            catch
            {
                Console.WriteLine("no Load function to call.");
                return false;
            }

            return false;
        }
        public bool Save(string filename)
        {
            string extension = Path.GetExtension(filename);

            if (extension == ".xml")
            {
                return SaveAsXML(filename);
            }
            else if (extension == ".csv")
            {
                return SaveAsCSV(filename);
            }
            else if (extension == ".json")
            {
                return SaveAsJson(filename);
            }

            return false;
        }
        public int GetHighestEffectiveRange()
        {
            this.Sort(Weapon.CompareByEffectiveRange);
            var weapon = this[this.Count - 1];
            return weapon.EffectiveRange;
        }
        public int GetLowestEffectiveRange()
        {
            this.Sort(Weapon.CompareByEffectiveRange);
            var weapon = this[0];
            return weapon.EffectiveRange;
        }

        public List<Weapon> GetAllWeaponsOfType(weapontype type)
        {
            List<Weapon> weapon = new List<Weapon>();
            foreach (var line in this)
            {
                if (line.Type == type)
                {
                    weapon.Add(line);
                }
            }
            return weapon;
        }
        public List<Weapon> GetAllWeaponsOfCaliber(int stars)
        {
            List<Weapon> weapon = new List<Weapon>();
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Caliber == stars)
                {
                    weapon.Add(this[i]);
                }
            }
            return weapon;
        }

        public void SortBy(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "name":
                    {
                        this.Sort(Weapon.CompareByName);
                        break;
                    }
                case "type":
                    {
                        this.Sort(Weapon.CompareByType);
                        break;
                    }
                case "effectiverange":
                    {
                        this.Sort(Weapon.CompareByEffectiveRange);
                        break;
                    }
                case "caliber":
                    {
                        this.Sort(Weapon.CompareByCaliber);
                        break;
                    }
                case "designedyear":
                    {
                        this.Sort(Weapon.CompareByDesignedYear);
                        break;
                    }
                case "country":
                    {
                        this.Sort(Weapon.CompareByCountry);
                        break;
                    }

                default:
                    Console.WriteLine("invalid");
                    break;
            }
        }

        public bool SaveAsXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(WeaponCollection));

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    xml.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool LoadXML(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(WeaponCollection));
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.Clear();
                    this.AddRange((WeaponCollection)xml.Deserialize(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool SaveAsJson(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(JsonSerializer.Serialize<WeaponCollection>(this));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool LoadJson(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    this.Clear();
                    this.AddRange(JsonSerializer.Deserialize<WeaponCollection>(reader.ReadToEnd()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool SaveAsCSV(string path)
        {
            FileStream fs;

            if (!File.Exists(path))
            {
                fs = File.Open(path, FileMode.Create);
            }
            else
            {
                fs = File.Open(path, FileMode.Append);
            }
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name,Type,EffectiveRange,Caliber,Image,DesignedYear,Country");
                foreach (var line in this)
                {
                    writer.WriteLine(line);
                }
            }
            return true;
        }

        public bool LoadCSV(string path)
        {
            this.Clear();
            if (!File.Exists(path))
            {
                return false;
            }
            using (StreamReader reader = new StreamReader(path))
            {
                string header = reader.ReadLine();
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    Weapon weapon = new Weapon();
                    if (Weapon.TryParse(line, out weapon))
                    {
                        this.Add(weapon);
                    }
                }
                return true;
            }
        }


    }
}
