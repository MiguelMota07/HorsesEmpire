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

			Horse horse = new Horse
			{
				Id = 1,
				Name = "Horse 1",
				BaseProduction = 10,
				BuyPrice = 10000,
				SellPrice = 9000,
				IsSold = false,
				EquipmentIds = new List<int>()
			};

			Info.Horses.Add(horse);
        }
    }
}
