using System;

namespace HorsesEmpire
{
    public partial class MainPage : ContentPage
    {
        int count = 0; int moneyclick=1; int walletmoney;

        public MainPage()
        {
            InitializeComponent();

        }
        public void Click(object sender, EventArgs e)
        {
            count++;
            walletmoney += moneyclick;
            money.Text=walletmoney.ToString()+"€";

        }

       

    }

}
