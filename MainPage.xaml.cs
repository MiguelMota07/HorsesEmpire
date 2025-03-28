using System;

namespace HorsesEmpire
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            moneyperclick.Text = Info.MoneyPerClick.ToString() + "€/click";
            moneypersecond.Text = Info.MoneyPerSecond.ToString() + "€/s";
            upgradeclick.Text = "Upgrade Click " + Info.ClickUpgradeCost.ToString() + "€";
        }

		public void OnClickMoneyButton(object sender, EventArgs e)
		{
            Info.Money += Info.MoneyPerClick;
            Info.AllMoney += Info.MoneyPerClick;
            Info.ClickNumber++;

            money.Text = Info.Money.ToString() + "€";
        }

		public void OnClickUpgradeButon(object sender, EventArgs e)
		{
            float upcost = Info.ClickUpgradeCost;
            int Money = Info.Money;

            if (Money > upcost) {
                Info.Money -= (int)upcost;
                if (Info.MoneyPerClick == 1)
                    Info.MoneyPerClick = 2;
                else
                    Info.MoneyPerClick = (int)(Info.MoneyPerClick * 1.5);
                Info.ClickUpgradeCost = (int)(upcost * 2);

                moneyperclick.Text = Info.MoneyPerClick.ToString() + "€/click";
                money.Text = Info.Money.ToString() + "€";
                upgradeclick.Text = "Upgrade Click " + Info.ClickUpgradeCost.ToString() + "€";
            }

            
        }
    }

}
