using System;

namespace HorsesEmpire;

public partial class StatusPage : ContentPage
{
	public StatusPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        numerofclicks.Text = Info.ClickNumber.ToString();
        allthemoney.Text = Info.AllMoney.ToString() + "€";

        int money=0;
        foreach (var horse in Info.Horses)
        {
            if (horse.IsSold == true)
            {
                money += horse.BuyPrice;
            }
        }

        assets.Text = $"{money.ToString()}€";
    }
    public void DeleteUserData(object sender, EventArgs e)
    {
        GameData gameData = new GameData();
        gameData.DeleteGameData();
        OnAppearing();
    }
}