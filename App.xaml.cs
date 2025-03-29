using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

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

            GameData gameData = new GameData();
            gameData.InitUserData();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            // Add your cleanup code here
            Console.WriteLine("Application is going to sleep");
            // Save any unsaved data
            GameData gameData = new GameData();
            gameData.SaveGameData();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            
            window.Destroying += Window_Destroying;
            
            return window;
        }

        private void Window_Destroying(object sender, EventArgs e)
        {
            // This is called when the window is being destroyed
            Console.WriteLine("Window is being destroyed");
            // Perform cleanup operations
            GameData gameData = new GameData();
            gameData.SaveGameData();
        }
    }
}
