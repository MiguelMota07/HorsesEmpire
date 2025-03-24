using System;

namespace HorsesEmpire
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        public async void gostatistics (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatusPage());
        }

    }

}
