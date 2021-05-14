using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Assignment2b
{
    [TestFixture]
    public class UnitTests
    {
        private WeaponCollection weaponcollection;
        private WeaponCollection empty;

        private string inputPath;
        private string outputPath;

        const string INPUT_FILE = "data2.csv";
        const string OUTPUT_FILE = "output.csv";

        // A helper function to get the directory of where the actual path is.
        private string CombineToAppPath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }

        [SetUp]
        public void SetUp()
        {
            inputPath = CombineToAppPath(INPUT_FILE);
            outputPath = CombineToAppPath(OUTPUT_FILE);
            weaponcollection = new WeaponCollection();
            empty = new WeaponCollection();
            weaponcollection.Load(inputPath);
        }

        [TearDown]
        public void CleanUp()
        {
            // We remove the output file after we are done.
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            if (File.Exists("weapons.json"))
            {
                File.Delete("weapons.json");
            }
            if (File.Exists("weapons.xml"))
            {
                File.Delete("weapons.xml");
            }
            if (File.Exists("weapons.csv"))
            {
                File.Delete("weapons.csv");
            }
            if (File.Exists("empty.json"))
            {
                File.Delete("empty.json");
            }
            if (File.Exists("empty.xml"))
            {
                File.Delete("empty.xml");
            }
            if (File.Exists("empty.csv"))
            {
                File.Delete("empty.csv");
            }
        }

        // WeaponCollection Unit Tests
        [Test]
        public void WeaponCollection_GetHighestBaseAttack_HighestValue()
        {
            // Expected Value: 48
            // TODO: call WeaponCollection.GetHighestBaseAttack() and confirm that it matches the expected value using asserts.
            Assert.IsTrue(weaponcollection.GetHighestBaseAttack() == 48);
        }

        [Test]
        public void WeaponCollection_GetLowestBaseAttack_LowestValue()
        {
            // Expected Value: 23
            // TODO: call WeaponCollection.GetLowestBaseAttack() and confirm that it matches the expected value using asserts.
            Assert.IsTrue(weaponcollection.GetLowestBaseAttack() == 23);
        }

        [TestCase(weapontype.Sword, 21)]
        public void WeaponCollection_GetAllWeaponsOfType_ListOfWeapons(weapontype type, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfType(type) and confirm that the weapons list returns Count matches the expected value using asserts.
            List<Weapon> weapon = weaponcollection.GetAllWeaponsOfType(type);
            //int count = 0;
            foreach (var line in weapon)
            {
                Assert.AreEqual(line.Type, type);
                //if (line.Type == type)
                //{
                //    count++;
                //}
            }

            Assert.AreEqual(weapon.Count, expectedValue);
        }

        [TestCase(5, 10)]
        public void WeaponCollection_GetAllWeaponsOfRarity_ListOfWeapons(int stars, int expectedValue)
        {
            // TODO: call WeaponCollection.GetAllWeaponsOfRarity(stars) and confirm that the weapons list returns Count matches the expected value using asserts.
            List<Weapon> weapon = weaponcollection.GetAllWeaponsOfRarity(stars);
            Assert.AreEqual(expectedValue, weapon.Count);
        }

        [Test]
        public void WeaponCollection_LoadThatExistAndValid_True()
        {
            // TODO: load returns true, expect WeaponCollection with count of 95 .
            weaponcollection.Clear();
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.AreEqual(weaponcollection.Count, 95);
        }

        [Test]
        public void WeaponCollection_LoadThatDoesNotExist_FalseAndEmpty()
        {
            // TODO: load returns false, expect an empty WeaponCollection
            weaponcollection.Clear();
            Assert.IsFalse(weaponcollection.Load("Alex"));
            Assert.AreEqual(weaponcollection.Count, 0);
        }

        //ERROR -2. Test Error.
        //public void WeaponCollection_SaveWithValuesCanLoad_TrueAndNotEmpty()
        //{
        //    // Load the data
        //    weaponCollection.Load(inputPath);
        //    weaponCollection.Save(outputPath);

        //    // Save returns true, load returns true, and WeaponCollection is not empty.
        //    Assert.IsTrue(weaponCollection.Load(inputPath));
        //    Assert.IsTrue(weaponCollection.Save(outputPath));
        //    Assert.AreNotEqual(weaponCollection.Count, 0);
        //}

        [Test]
        public void WeaponCollection_SaveWithValuesCanLoad_TrueAndNotEmpty()
        {
            // Load the data
            weaponcollection.Load(inputPath);
            weaponcollection.Save(outputPath);

            // Save returns true, load returns true, and WeaponCollection is not empty.
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.IsTrue(weaponcollection.Save(outputPath));
            Assert.AreNotEqual(weaponcollection.Count, 0);
        }

        [Test]
        public void WeaponCollection_SaveEmpty_TrueAndEmpty()
        {
            // After saving an empty WeaponCollection, load the file and expect WeaponCollection to be empty.
            weaponcollection.Clear();
            Assert.IsTrue(weaponcollection.Save(outputPath));
            Assert.IsTrue(weaponcollection.Load(outputPath));
            Assert.IsTrue(weaponcollection.Count == 0);
        }

        // Weapon Unit Tests
        [Test]
        public void Weapon_TryParseValidLine_TruePropertiesSet()
        {
            // TODO: create a Weapon with the stats above set properly

            Weapon expected = null;
            // TODO: uncomment this once you added the Type1 and Type2
            expected = new Weapon()
            {
                Name = "Skyward Blade",
                Type = weapontype.Sword,
                Image = "https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png",
                Rarity = 5,
                BaseAttack = 46,
                SecondaryStat = "Energy Recharge",
                Passive = "Sky-Piercing Fang"
            };

            string line = "Skyward Blade,Sword,https://vignette.wikia.nocookie.net/gensin-impact/images/0/03/Weapon_Skyward_Blade.png,5,46,Energy Recharge,Sky-Piercing Fang";
            Weapon actual = null;

            // TODO: uncomment this once you have TryParse implemented.
            Assert.IsTrue(Weapon.TryParse(line, out actual));
            Assert.IsTrue(expected.Name == actual.Name);
            Assert.IsTrue(expected.Type == actual.Type);
            Assert.IsTrue(expected.BaseAttack == actual.BaseAttack);
            Assert.IsTrue(expected.Image == actual.Image);
            Assert.IsTrue(expected.Rarity == actual.Rarity);
            Assert.IsTrue(expected.SecondaryStat == actual.SecondaryStat);
            Assert.IsTrue(expected.Passive == actual.Passive);
            // TODO: check for the rest of the properties, Image,Rarity,SecondaryStat,Passive
        }

        [Test]
        public void Weapon_TryParseInvalidLine_FalseNull()
        {
            // TODO: use "1,Bulbasaur,A,B,C,65,65", Weapon.TryParse returns false, and Weapon is null.
            string line = "1,Bulbasaur,A,B,C,65,65";
            Weapon actual = null;
            Assert.IsFalse(Weapon.TryParse(line, out actual));
        }

        [Test]
        public void WeaponCollection_Load_Save_Load_ValidJson()
        {
            outputPath = CombineToAppPath("weapons.json");
            inputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponcollection.Save(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_Load_ValidJson()
        {
            outputPath = CombineToAppPath("weapons.json");
            inputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponcollection.SaveAsJson(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_SaveAsJSON_LoadJSON_ValidJson()
        {
            outputPath = CombineToAppPath("weapons.json");
            inputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponcollection.SaveAsJson(outputPath));
            Assert.IsTrue(weaponcollection.LoadJson(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_Save_LoadJSON_ValidJson()
        {
            outputPath = CombineToAppPath("weapons.json");
            inputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(weaponcollection.Save(outputPath));
            Assert.IsTrue(weaponcollection.LoadJson(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 95);
        }

        [Test]
        public void WeaponCollection_Load_Save_Load_ValidCsv()
        {
            outputPath = CombineToAppPath("weapons.csv");
            inputPath = CombineToAppPath("weapons.csv");
            Assert.IsTrue(weaponcollection.Save(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
        }

        [Test]
        public void WeaponCollection_Load_SaveAsCSV_LoadCSV_ValidCsv()
        {
            outputPath = CombineToAppPath("weapons.csv");
            inputPath = CombineToAppPath("weapons.csv");
            Assert.IsTrue(weaponcollection.SaveAsCSV(outputPath));
            Assert.IsTrue(weaponcollection.LoadCSV(inputPath));
        }

        [Test]
        public void WeaponCollection_Load_Save_Load_ValidXml()
        {
            outputPath = CombineToAppPath("weapons.csv");
            inputPath = CombineToAppPath("weapons.csv");
            Assert.IsTrue(weaponcollection.Save(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
        }

        [Test]
        public void WeaponCollection_Load_SaveAsXML_LoadXML_ValidXml()
        {
            outputPath = CombineToAppPath("weapons.csv");
            inputPath = CombineToAppPath("weapons.csv");
            Assert.IsTrue(weaponcollection.SaveAsXML(outputPath));
            Assert.IsTrue(weaponcollection.LoadXML(inputPath));
        }

        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidJson()
        {
            outputPath = CombineToAppPath("empty.json");
            inputPath = CombineToAppPath("empty.json");
            Assert.IsTrue(empty.SaveAsJson(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }

        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidCsv()
        {
            outputPath = CombineToAppPath("empty.csv");
            inputPath = CombineToAppPath("empty.csv");
            Assert.IsTrue(empty.SaveAsCSV(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }

        [Test]
        public void WeaponCollection_SaveEmpty_Load_ValidXml()
        {
            outputPath = CombineToAppPath("empty.xml");
            inputPath = CombineToAppPath("empty.xml");
            Assert.IsTrue(empty.SaveAsXML(outputPath));
            Assert.IsTrue(weaponcollection.Load(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }

        [Test]
        public void WeaponCollection_Load_SaveJSON_LoadXML_InvalidXml()
        {
            outputPath = CombineToAppPath("weapons.json");
            inputPath = CombineToAppPath("weapons.json");
            Assert.IsTrue(empty.SaveAsJson(outputPath));
            Assert.IsFalse(weaponcollection.LoadXML(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }

        [Test]
        public void WeaponCollection_Load_SaveXML_LoadJSON_InvalidJson()
        {
            outputPath = CombineToAppPath("weapons.xml");
            inputPath = CombineToAppPath("weapons.xml");
            Assert.IsTrue(empty.SaveAsXML(outputPath));
            Assert.IsFalse(weaponcollection.LoadJson(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadXML_InvalidXml()
        {
            Assert.IsFalse(weaponcollection.LoadXML(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }

        [Test]
        public void WeaponCollection_ValidCsv_LoadJSON_InvalidJson()
        {
            Assert.IsFalse(weaponcollection.LoadJson(inputPath));
            Assert.IsTrue(weaponcollection.Count() == 0);
        }
    }
}
