using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment2a
{
    public class WeaponCollection: List<Weapon>, IPeristence
    {
        public bool Load(string filename)
        {
            //this.Clear();
            if (!File.Exists(filename))
            {
                return false;
            }
            using (StreamReader reader = new StreamReader(filename))
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
        public bool Save(string filename)
        {
            FileStream fs;
            fs = File.Open(filename, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine("Name,Type,Rarity,BaseAttack,Image,Passive,SecondaryStat");
                foreach (var line in this)
                {
                    writer.WriteLine(line);
                }
            }
            return true;
        }
        public int GetHighestBaseAttack()
        {
            this.Sort(Weapon.CompareByBaseAttack);
            var weapon = this[this.Count - 1];
            return weapon.BaseAttack;
        }
        public int GetLowestBaseAttack()
        {
            this.Sort(Weapon.CompareByBaseAttack);
            var weapon = this[0];
            return weapon.BaseAttack;
        }
        public List<Weapon> GetAllWeaponsOfType(weapontype type)
        {
            List<Weapon> weapon = new List<Weapon>();
            //for(int i = 0; i < this.Count; i++)
            //{
            //    if(this[i].Type == type)
            //    {
            //        weapon.Add(this[i]);
            //    }
            //}
            foreach(var line in this)
            {
                if(line.Type == type)
                {
                    weapon.Add(line);
                }
            }
            return weapon;
        }
        public List<Weapon> GetAllWeaponsOfRarity(int stars)
        {
            List<Weapon> weapon = new List<Weapon>();
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Rarity == stars)
                {
                    weapon.Add(this[i]);
                }
            }
            return weapon;
        }
        void SortBy(string columnName)
        {
            switch(columnName)
            {
                case "Name":
                    {
                        this.Sort(Weapon.CompareByName);
                        break;
                    }
                case "Type":
                    {
                        this.Sort(Weapon.CompareByType);
                        break;
                    }
                case "Rarity":
                    {
                        this.Sort(Weapon.CompareByRarity);
                        break;
                    }
                case "BaseAttack":
                    {
                        this.Sort(Weapon.CompareByBaseAttack);
                        break;
                    }
                default:
                    Console.WriteLine("invalid");
                    break;
            }
        }
    }
}
