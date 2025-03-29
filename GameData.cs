using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;



namespace HorsesEmpire
{
    public class GameData
    {
        private readonly string Fich = Path.Combine(FileSystem.AppDataDirectory, "gamedata.xml");
        public void InitUserData()
        {
            if (File.Exists(Fich) == true)             
            {
                loadFromFile();
            }
            else
            {
                XmlTextWriter FicheiroXml = new XmlTextWriter(Fich, System.Text.Encoding.UTF8);
                FicheiroXml.WriteStartDocument(true);                  
                // Definir o estilo de indentação do ficheiro                  
                FicheiroXml.Formatting = Formatting.Indented;                 
                FicheiroXml.Indentation = 2;                  
                // Criar a raiz             

                FicheiroXml.WriteStartElement("gamedata"); // Add root element
                
                FicheiroXml.WriteStartElement("userdata");
                FicheiroXml.WriteEndElement();
                
                FicheiroXml.WriteStartElement("horses");
                FicheiroXml.WriteEndElement();
                
                FicheiroXml.WriteStartElement("equipment");
                FicheiroXml.WriteEndElement();
                
                FicheiroXml.WriteEndElement(); // Close root element
            
                FicheiroXml.Close();

                insertToFile();

                loadFromFile();
            }   
        }

        private void insertToFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Fich);

            //userdata
            XmlNode userdata = doc.SelectSingleNode("//userdata");

            //user attributes
            XmlAttribute money = doc.CreateAttribute("money");
            money.Value = "0";
            userdata.Attributes.Append(money);

            XmlAttribute moneyPerClick = doc.CreateAttribute("moneyPerClick");
            moneyPerClick.Value = "1";
            userdata.Attributes.Append(moneyPerClick);

            XmlAttribute moneyPerSecond = doc.CreateAttribute("moneyPerSecond");
            moneyPerSecond.Value = "0";
            userdata.Attributes.Append(moneyPerSecond);

            XmlAttribute allMoney = doc.CreateAttribute("allMoney");
            allMoney.Value = "0";
            userdata.Attributes.Append(allMoney);

            XmlAttribute clickUpgradeCost = doc.CreateAttribute("clickUpgradeCost");
            clickUpgradeCost.Value = "40";
            userdata.Attributes.Append(clickUpgradeCost);

            XmlAttribute clickNumber = doc.CreateAttribute("clickNumber");
            clickNumber.Value = "0";
            userdata.Attributes.Append(clickNumber);

            XmlAttribute horsesSpaces = doc.CreateAttribute("horsesSpaces");
            horsesSpaces.Value = "5";
            userdata.Attributes.Append(horsesSpaces);

            XmlAttribute horsesSpacesPrice = doc.CreateAttribute("horsesSpacesPrice");
            horsesSpacesPrice.Value = "20000";
            userdata.Attributes.Append(horsesSpacesPrice);

            XmlAttribute lastSave = doc.CreateAttribute("lastSave");
            lastSave.Value = Info.GetCurrentDateTime().ToString();
            userdata.Attributes.Append(lastSave);

            // Save the changes to the file
            

            //horses
            XmlNode horses = doc.SelectSingleNode("//horses");

            List<Horse> horseList = new List<Horse>();

            loadHorseList(horseList);

            foreach (Horse h in horseList)
            {
                XmlElement horseElement = doc.CreateElement("horse");
                horses.AppendChild(horseElement);
                horseElement.SetAttribute("id", h.Id.ToString());
                horseElement.SetAttribute("name", h.Name);
                horseElement.SetAttribute("baseProduction", h.BaseProduction.ToString());
                horseElement.SetAttribute("buyPrice", h.BuyPrice.ToString());
                horseElement.SetAttribute("sellPrice", h.SellPrice.ToString());
                horseElement.SetAttribute("isSold", h.IsSold.ToString());

                string equipments = string.Join(",", h.Equipments.Select(e => e.Id));
                horseElement.SetAttribute("equipments", equipments);

                horses.AppendChild(horseElement);
            }

            //Equipment
            XmlNode equipment = doc.SelectSingleNode("//equipment");

            List<Equipment> equipmentList = new List<Equipment>();
            loadEquipmentList(equipmentList);

            foreach (Equipment e in equipmentList)
            {
                XmlElement equipmentElement = doc.CreateElement("equipment");
                equipment.AppendChild(equipmentElement);
                equipmentElement.SetAttribute("id", e.Id.ToString());
                equipmentElement.SetAttribute("name", e.Name);
                equipmentElement.SetAttribute("multiplier", e.Multiplier.ToString());
                equipmentElement.SetAttribute("price", e.Price.ToString());
                equipmentElement.SetAttribute("soldAmount", e.SoldAmount.ToString());
                equipmentElement.SetAttribute("inUse", e.InUse.ToString());

                equipment.AppendChild(equipmentElement);
            }

            doc.Save(Fich);
        }

        private void loadFromFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Fich);

            // Load user data
            XmlNode userdata = doc.SelectSingleNode("//userdata");
            if (userdata != null)
            {
                Info.Money = int.Parse(userdata.Attributes["money"].Value);
                Info.MoneyPerClick = int.Parse(userdata.Attributes["moneyPerClick"].Value);
                Info.MoneyPerSecond = int.Parse(userdata.Attributes["moneyPerSecond"].Value);
                Info.AllMoney = int.Parse(userdata.Attributes["allMoney"].Value);
                Info.ClickUpgradeCost = int.Parse(userdata.Attributes["clickUpgradeCost"].Value);
                Info.ClickNumber = int.Parse(userdata.Attributes["clickNumber"].Value);
                Info.HorsesSpaces = int.Parse(userdata.Attributes["horsesSpaces"].Value);
                Info.LastSave = long.Parse(userdata.Attributes["lastSave"].Value);
                Info.HorsesSpacesPrice = int.Parse(userdata.Attributes["horsesSpacesPrice"].Value);
            }

            // Load equipments
            XmlNodeList equipmentNodes = doc.SelectNodes("//equipment/equipment");
            foreach (XmlNode equipmentNode in equipmentNodes)
            {
                Equipment equipment = new Equipment(
                    int.Parse(equipmentNode.Attributes["id"].Value),
                    equipmentNode.Attributes["name"].Value,
                    double.Parse(equipmentNode.Attributes["multiplier"].Value),
                    int.Parse(equipmentNode.Attributes["price"].Value),
                    int.Parse(equipmentNode.Attributes["soldAmount"].Value),
                    int.Parse(equipmentNode.Attributes["inUse"].Value)
                );
                Info.Equipments.Add(equipment);
            }

            // Load horses
            XmlNodeList horseNodes = doc.SelectNodes("//horses/horse");
            foreach (XmlNode horseNode in horseNodes)
            {
                List<Equipment> horseEquipments = new List<Equipment>();
                string[] equipmentIds = horseNode.Attributes["equipments"].Value.Split(',');
                foreach (string id in equipmentIds)
                {
                    if(string.IsNullOrEmpty(id)) continue;
                    int equipmentId = int.Parse(id);
                    Equipment equipment = Info.Equipments.FirstOrDefault(e => e.Id == equipmentId);
                    if (equipment != null)
                    {
                        horseEquipments.Add(equipment);
                    }
                }

                Horse horse = new Horse(
                    int.Parse(horseNode.Attributes["id"].Value),
                    horseNode.Attributes["name"].Value,
                    int.Parse(horseNode.Attributes["baseProduction"].Value),
                    int.Parse(horseNode.Attributes["buyPrice"].Value),
                    int.Parse(horseNode.Attributes["sellPrice"].Value),
                    bool.Parse(horseNode.Attributes["isSold"].Value),
                    horseEquipments

                );
                Info.Horses.Add(horse);
            }
        }
        
        public void SaveGameData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Fich);

            // Save user data
            XmlNode userdata = doc.SelectSingleNode("//userdata");
            if (userdata != null)
            {
                userdata.Attributes["money"].Value = Info.Money.ToString();
                userdata.Attributes["moneyPerClick"].Value = Info.MoneyPerClick.ToString();
                userdata.Attributes["moneyPerSecond"].Value = Info.MoneyPerSecond.ToString();
                userdata.Attributes["allMoney"].Value = Info.AllMoney.ToString();
                userdata.Attributes["clickUpgradeCost"].Value = Info.ClickUpgradeCost.ToString();
                userdata.Attributes["clickNumber"].Value = Info.ClickNumber.ToString();
                userdata.Attributes["horsesSpaces"].Value = Info.HorsesSpaces.ToString();
                userdata.Attributes["lastSave"].Value = Info.LastSave.ToString();
                userdata.Attributes["horsesSpacesPrice"].Value = Info.HorsesSpacesPrice.ToString();
            }

            // Save horses
            XmlNode horses = doc.SelectSingleNode("//horses");
            if (horses != null)
            {
                horses.RemoveAll(); // Clear existing horse nodes

                foreach (Horse h in Info.Horses)
                {
                    XmlElement horseElement = doc.CreateElement("horse");
                    horses.AppendChild(horseElement);
                    horseElement.SetAttribute("id", h.Id.ToString());
                    horseElement.SetAttribute("name", h.Name);
                    horseElement.SetAttribute("baseProduction", h.BaseProduction.ToString());
                    horseElement.SetAttribute("buyPrice", h.BuyPrice.ToString());
                    horseElement.SetAttribute("sellPrice", h.SellPrice.ToString());
                    horseElement.SetAttribute("isSold", h.IsSold.ToString());

                    string equipments = string.Join(",", h.Equipments.Select(e => e.Id));
                    horseElement.SetAttribute("equipments", equipments);

                    horses.AppendChild(horseElement);
                }
            }

            // Save equipment
            XmlNode equipment = doc.SelectSingleNode("//equipment");
            if (equipment != null)
            {
                equipment.RemoveAll(); // Clear existing equipment nodes

                foreach (Equipment e in Info.Equipments)
                {
                    XmlElement equipmentElement = doc.CreateElement("equipment");
                    equipment.AppendChild(equipmentElement);
                    equipmentElement.SetAttribute("id", e.Id.ToString());
                    equipmentElement.SetAttribute("name", e.Name);
                    equipmentElement.SetAttribute("multiplier", e.Multiplier.ToString());
                    equipmentElement.SetAttribute("price", e.Price.ToString());
                    equipmentElement.SetAttribute("soldAmount", e.SoldAmount.ToString());
                    equipmentElement.SetAttribute("inUse", e.InUse.ToString());

                    equipment.AppendChild(equipmentElement);
                }
            }

            doc.Save(Fich);
        }

        public void DeleteGameData()
        {
            if (File.Exists(Fich))
            {
                Info.Horses.Clear();
                Info.Equipments.Clear();
                File.Delete(Fich);
                InitUserData();
            }
        }


        private void loadHorseList(List<Horse> horseList)
        {
            // horseList.Add(new Horse(Id, Nome, BaseProduction, BuyPrice, SellPrice, IsSold, Equipments));
            horseList.Add(new Horse(0, "Boneca", 1, 3000, 2000, false, new List<Equipment>()));
            horseList.Add(new Horse(1, "Geada", 2, 4000, 3000, false, new List<Equipment>()));
            horseList.Add(new Horse(2, "Divino", 3, 6000, 4500, false, new List<Equipment>()));
            horseList.Add(new Horse(3, "Dubai", 4, 7000, 5500, false, new List<Equipment>()));
            horseList.Add(new Horse(4, "Rato", 6, 10000, 7000, false, new List<Equipment>()));
            horseList.Add(new Horse(5, "Boneco", 10, 12500, 9000, false, new List<Equipment>()));
            horseList.Add(new Horse(6, "Digno", 13, 15000, 12000, false, new List<Equipment>()));
            horseList.Add(new Horse(7, "Hindoctroboy", 15, 20000, 15000, false, new List<Equipment>()));
            horseList.Add(new Horse(8, "JailhouseRock", 20, 30000, 20000, false, new List<Equipment>()));
            horseList.Add(new Horse(9, "Orfeu II", 25, 37000, 30000, false, new List<Equipment>()));
            horseList.Add(new Horse(10, "Vera", 30, 50000, 40000, false, new List<Equipment>()));
            horseList.Add(new Horse(11, "Falcão", 35, 60000, 50000, false, new List<Equipment>()));
            horseList.Add(new Horse(12, "Jenifer", 40, 70000, 60000, false, new List<Equipment>()));
            horseList.Add(new Horse(13, "Luna", 50, 80000, 70000, false, new List<Equipment>()));
            horseList.Add(new Horse(14, "Trajado", 60, 90000, 80000, false, new List<Equipment>()));
            horseList.Add(new Horse(15, "Indio", 70, 100000, 90000, false, new List<Equipment>()));
            horseList.Add(new Horse(16, "Julieta", 80, 110000, 100000, false, new List<Equipment>()));
            horseList.Add(new Horse(17, "Hera", 90, 120000, 110000, false, new List<Equipment>()));
            horseList.Add(new Horse(18, "Malibu", 100, 130000, 120000, false, new List<Equipment>()));
            horseList.Add(new Horse(19, "Caramelo", 110, 140000, 130000, false, new List<Equipment>()));
            horseList.Add(new Horse(20, "Massive", 120, 150000, 140000, false, new List<Equipment>()));
            horseList.Add(new Horse(21, "Triunfo", 300, 1500000, 1400000, false, new List<Equipment>()));
        }
        private void loadEquipmentList(List<Equipment> equipmentList)
        {
            // equipmentList.Add(new Equipment(Id, Nome, Multiplier, Price, SoldAmount, InUse));
            equipmentList.Add(new Equipment(0, "Sela partida", 1.5, 500, 0, 0));
            equipmentList.Add(new Equipment(1, "Sela Dura Suja", 2.0, 1000, 0, 0));
            equipmentList.Add(new Equipment(2, "Sela Dura", 2.5, 2000, 0, 0));
            equipmentList.Add(new Equipment(3, "Sela Nomal Suja", 3.0, 4000, 0, 0));
            equipmentList.Add(new Equipment(4, "Sela Normal", 3.5, 6000, 0, 0));
            equipmentList.Add(new Equipment(5, "Sela de Couro", 4.0, 10000, 0, 0));
            equipmentList.Add(new Equipment(6, "Sela Macia", 4.5, 17000, 0, 0));
            equipmentList.Add(new Equipment(7, "Sela Profissional", 10.0, 100000, 0, 0));
        }

        public void updateMoney(bool updated = true)
		{
            long currentTime = Info.GetCurrentDateTime();
            long lastUpdateTime = Info.LastSave;         
            // Calculate elapsed time since last update
            long secundsElapsed = currentTime - lastUpdateTime;

            double moneyEarned = 0;

            if (updated == false)
            {
                moneyEarned = Info.MoneyPerSecond * secundsElapsed;
            }
            else{
                // Calculate money earned based on horses and equipment

                double incomepersecond = 0;
                foreach (var horse in Info.Horses.Where(x => x.IsSold == true))
                {
                    double baseIncome = horse.BaseProduction;

                    // Apply multipliers from equipment
                    double equipmentMultiplier = 1;
                    foreach (var equipment in horse.Equipments)
                    {
                        equipmentMultiplier *= equipment.Multiplier;
                    }

                    // Calculate income for this horse based on time elapsed
                    double horseIncome = baseIncome * secundsElapsed * equipmentMultiplier;
                    moneyEarned += horseIncome;
                    incomepersecond += baseIncome*equipmentMultiplier;

                    
                }
                Info.MoneyPerSecond = (int)incomepersecond;
            }

            // Update user's money
            Info.Money += (int)moneyEarned;
            Info.AllMoney += (int)moneyEarned;

            // Update the last update time
            Info.LastSave = currentTime;
		}
    }
}