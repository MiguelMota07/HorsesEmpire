namespace HorsesEmpire
{
    public partial class App : Application
    {
        public static int Money { get; set; }
        public static int MoneyPerClick { get; set; }
        public static int MoneyPerSecond { get; set; }
        public static int ClickUpgradeCost { get; set; }
        public static int AllMoney { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            UserAppTheme = AppTheme.Light;

            InitUserData();
        }

        private void InitUserData()
        {
            Money = 0;
            MoneyPerClick = 1;
            MoneyPerSecond = 0;
            AllMoney = 0;
            ClickUpgradeCost = 40;
        }
    }
}
