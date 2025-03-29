using System.Xml;

namespace HorsesEmpire
{
    public partial class App : Application
    {
        //public static Info Info { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //Info = new Info();
            UserAppTheme = AppTheme.Light;

            InitUserData();
        }

        private void InitUserData()
        {
			// string Fich = "info.xml";

			// if (File.Exists(Fich) == false)             
			// {                 
			// 	XmlTextWriter FicheiroXml = new XmlTextWriter(Fich, System.Text.Encoding.UTF8);                  
			// 	//Criar um novo ficheiro                 
			// 	FicheiroXml.WriteStartDocument(true);                  
			// 	// Definir o estilo de indentação do ficheiro                  
			// 	FicheiroXml.Formatting = Formatting.Indented;                 
			// 	FicheiroXml.Indentation = 2;                  
			// 	// Criar a raiz                  
			// 	FicheiroXml.WriteStartElement("associacao");                  
			// 	// Escrever a estrutura no ficheiro                  
			// 	FicheiroXml.WriteEndDocument();                 
			// 	FicheiroXml.Close();             
			// }          
		




            Info.Money = 0;
            Info.MoneyPerClick = 1;
            Info.MoneyPerSecond = 0;
            Info.AllMoney = 0;
            Info.ClickUpgradeCost = 40;
			Info.ClickNumber = 0;
            Equipment equipment1 = new Equipment
            {
                Id = 1,
                Name = "Normal Saddle",
                Multiplier = 1.50,
                Price = 200,
                SoldAmount = 2,
                InUse = 1
            };
            Info.Equipment.Add(equipment1);
            Equipment equipment2 = new Equipment
            {
                Id = 2,
                Name = "Op Saddle",
                Multiplier = 5,
                Price = 2000,
                SoldAmount = 2,
                InUse = 2
            };
            Info.Equipment.Add(equipment2);
            Horse horse = new Horse
			{
				Id = 1,
				Name = "Horse 1",
				BaseProduction = 10,
				BuyPrice = 10000,
				SellPrice = 9000,
				IsSold = true,
				Equipments = new List<Equipment>() {equipment1, equipment2}
			};

			Info.Horses.Add(horse);
            horse = new Horse
            {
                Id = 2,
                Name = "Horse 2",
                BaseProduction = 10,
                BuyPrice = 10000,
                SellPrice = 9000,
                IsSold = true,
                Equipments = new List<Equipment>() {equipment2}
            };

            Info.Horses.Add(horse);
        }
    }
}
