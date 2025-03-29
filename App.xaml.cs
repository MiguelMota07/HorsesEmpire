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
            Info.Money = 0;
            Info.MoneyPerClick = 1;
            Info.MoneyPerSecond = 0;
            Info.AllMoney = 0;
            Info.ClickUpgradeCost = 40;
			Info.ClickNumber = 0;
            Equipment equipment = new Equipment
            {
                Id = 1,
                Name = "Normal Saddle",
                Multiplier = 1.50,
                Price = 200,
                SoldAmount = 2,
                InUse = 1
            };
            Info.Equipment.Add(equipment);
            equipment = new Equipment
            {
                Id = 2,
                Name = "Op Saddle",
                Multiplier = 5,
                Price = 2000,
                SoldAmount = 1,
                InUse = 1
            };
            Info.Equipment.Add(equipment);
            Horse horse = new Horse
			{
				Id = 1,
				Name = "Horse 1",
				BaseProduction = 10,
				BuyPrice = 10000,
				SellPrice = 9000,
				IsSold = true,
				EquipmentIds = new List<int>() { 1,2}
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
                EquipmentIds = new List<int>()
            };

            Info.Horses.Add(horse);
        }
    }
}
