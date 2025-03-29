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
        assets.Text = 0 + "€";
    }
    public void DeleteUserData(object sender, EventArgs e)
    {
        
    }
}