using System;

namespace HorsesEmpire
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            moneyperclick.Text = App.MoneyPerClick.ToString() + "€/click";
            moneypersecond.Text = App.MoneyPerSecond.ToString() + "€/s";
            upgradeclick.Text = "Upgrade Click " + App.ClickUpgradeCost.ToString() + "€";
        }

       public void OnClickMoneyButton(object sender, EventArgs e)
       {
            App.Money += App.MoneyPerClick;
            App.AllMoney += App.MoneyPerClick;
            
            money.Text = App.Money.ToString() + "€";
        }

       public void OnClickUpgradeButon(object sender, EventArgs e)
       {
            float upcost = App.ClickUpgradeCost;
            int Money = App.Money;

            if (Money > upcost) {
                App.Money -= (int)upcost;
                if (App.MoneyPerClick == 1)
                    App.MoneyPerClick = 2;
                else
                    App.MoneyPerClick = (int)(App.MoneyPerClick * 1.5);
                App.ClickUpgradeCost = (int)(upcost * 2);

                moneyperclick.Text = App.MoneyPerClick.ToString() + "€/click";
                money.Text = App.Money.ToString() + "€";
                upgradeclick.Text = App.ClickUpgradeCost.ToString() + "€";
            }
        }
    }

}
