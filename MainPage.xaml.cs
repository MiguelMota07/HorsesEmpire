using System;

namespace HorsesEmpire
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer _moneyUpdateTimer;
        public MainPage()
        {
            InitializeComponent();


            _moneyUpdateTimer = new System.Timers.Timer(10000); // 10 em 10 segundos
            _moneyUpdateTimer.Elapsed += UpdateMoney;
            _moneyUpdateTimer.AutoReset = true;
            _moneyUpdateTimer.Start();
        }
        private void UpdateMoney(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Dispatch(() =>
            {
                money.Text = Info.Money.ToString() + "€";
            });
        }
        protected override void OnAppearing()
        {
            moneyperclick.Text = Info.MoneyPerClick.ToString() + "€/clique";
            moneypersecond.Text = Info.MoneyPerSecond.ToString() + "€/s";
            upgradeclick.Text = "Melhorar Clique " + Info.ClickUpgradeCost.ToString() + "€";
            money.Text = Info.Money.ToString() + "€";
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

                moneyperclick.Text = Info.MoneyPerClick.ToString() + "€/clique";
                money.Text = Info.Money.ToString() + "€";
                upgradeclick.Text = "Melhorar Clique " + Info.ClickUpgradeCost.ToString() + "€";
            }

            
        }
    }

}
