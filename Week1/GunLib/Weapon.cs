using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunLib
{

    public enum weapontype
    {
        SemiAutomaticRifle,
        Pistol,
        AntiMaterielRifle,
        AssaultRifle,
        AutomaticRifle,
        BoltActionRifle,
        LeverActionRifle,
        Shotgun,
        MachineGun,
        SniperRifle,
        Revolver,
        FallingBlockRifle,
        SubmachineGun,
        None
    }

    public class Weapon
    {

        public string Name { get; set; }
        public weapontype Type { get; set; }
        public int EffectiveRange { get; set; }
        public float Caliber { get; set; }

        public string Image { get; set; }
        public int DesignedYear { get; set; }
        public string Country { get; set; }

       
        public static int CompareByName(Weapon left, Weapon right)
        {
            return left.Name.CompareTo(right.Name);
        }
        public static int CompareByType(Weapon left, Weapon right)
        {
            return left.Type.CompareTo(right.Type);
        }
        public static int CompareByEffectiveRange(Weapon left, Weapon right)
        {
            return left.EffectiveRange.CompareTo(right.EffectiveRange);
        }
        public static int CompareByCaliber(Weapon left, Weapon right)
        {
            return left.Caliber.CompareTo(right.Caliber);
        }
        public static int CompareByDesignedYear(Weapon left, Weapon right)
        {
            return left.DesignedYear.CompareTo(right.DesignedYear);
        }
        public static int CompareByCountry(Weapon left, Weapon right)
        {
            return left.Country.CompareTo(right.Country);
        }
        

        public static bool TryParse(string rawData, out Weapon weapon)
        {
            string[] values = rawData.Split(',');
            weapon = new Weapon();

           
            if (values.Length == 7)
            {
                try
                {
                    weapon.Name = values[0];
                    weapon.Type = Enum.Parse<weapontype>(values[1]);
                    weapon.Image = values[2];
                    weapon.EffectiveRange = int.Parse(values[3]);
                    weapon.Caliber = float.Parse(values[4]);
                    weapon.DesignedYear = int.Parse(values[5]);
                    weapon.Country = values[6];
                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to parse");
                    return false;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return string.Format("Name: {0}  Type: {1}  EffectiveRange: {2}meters  Caliber: {3}mm  Image: {4}  DesignedYear: {5}  Country: {6}", Name, Type, EffectiveRange, Caliber, Image, DesignedYear, Country);
            //string message = $@"{Name}, {Type}, {EffectiveRange}meters, {Caliber}mm, {Image}, {DesignedYear}, {Country}";
            //return message;
        }
    }
}
