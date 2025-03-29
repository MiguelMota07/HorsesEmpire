
namespace HorsesEmpire
{
    public partial class App : Application
    {
        //public static Info Info { get; set; }
        private System.Timers.Timer _moneyUpdateTimer;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //Info = new Info();
            UserAppTheme = AppTheme.Light;

            GameData gameData = new GameData();
            gameData.InitUserData();

            // Initialize and start the timer for updating money
            _moneyUpdateTimer = new System.Timers.Timer(10000); // 10 em 10 segundos
            _moneyUpdateTimer.Elapsed += UpdateUserMoney;
            _moneyUpdateTimer.AutoReset = true;
            _moneyUpdateTimer.Start();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            // Add your cleanup code here
            Console.WriteLine("Application is going to sleep");
            // Save any unsaved data
            GameData gameData = new GameData();
            gameData.SaveGameData();

            _moneyUpdateTimer?.Stop();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            
            window.Destroying += Window_Destroying;
            
            return window;
        }

        private void Window_Destroying(object sender, EventArgs e)
        {
            GameData gameData = new GameData();
            gameData.SaveGameData();

            _moneyUpdateTimer?.Stop();
        }

        protected override void OnResume()
        {
            base.OnResume();
            
            // Restart the timer when app resumes
            if (_moneyUpdateTimer != null && !_moneyUpdateTimer.Enabled)
            {
                _moneyUpdateTimer.Start();
                GameData gameData = new GameData();
                gameData.updateMoney(false);
            }
        }

        

        private void UpdateUserMoney(object sender, System.Timers.ElapsedEventArgs e)
        {
            GameData gameData = new GameData();
            gameData.updateMoney(false);
            Dispatcher.Dispatch(() =>
            {
                gameData.updateMoney(false);
            });
        }
    }
}
