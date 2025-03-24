using System;

namespace HorsesEmpire;

public partial class StatusPage : ContentPage
{
	public StatusPage()
	{
		InitializeComponent();
	}
    public async void gohome(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}