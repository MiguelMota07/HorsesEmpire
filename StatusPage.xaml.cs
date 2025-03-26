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
        numerofclicks.Text = App.ClickNumber.ToString();
        allthemoney.Text = App.AllMoney.ToString() + "€";
        assets.Text = 0 + "€";
    }

}