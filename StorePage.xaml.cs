using System.Diagnostics;
using System.Drawing;

namespace HorsesEmpire;

public partial class StorePage : ContentPage
{
	public StorePage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        horsesview.ItemsSource = Info.Horses.Where(x => x.IsSold == false).ToList();
        equipmentview.ItemsSource = Info.Equipments;
        int numHorses = Info.Horses.Count(x => x.IsSold == true);
        horsesamount.Text = $"{numHorses.ToString()}/{Info.HorsesSpaces}";
        BuySpacesButton.Text = $"Comprar +5 - {Info.HorsesSpacesPrice}€";
        GameData gameData = new GameData();
        gameData.updateMoney();
        BuySpacesButton.Text = $"Comprar +5 - {Info.HorsesSpacesPrice.ToString()}€";

    }
    public void switchstoretoequipment(object sender, EventArgs e)
    {
        if(sender is Border border)
        {
            border.BackgroundColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color950"]; ;
            switchhorselabel.TextColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color950"];
            switchhorse.BackgroundColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color50"];
            switchequipmentlabel.TextColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color50"];
            horsesview.IsVisible = false;
            equipmentview.IsVisible = true;
        }
        
    }
    public void switchstoretohorses(object sender, EventArgs e)
    {
        if (sender is Border border)
        {
            border.BackgroundColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color950"]; ;
            switchhorselabel.TextColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color50"];
            switchequipment.BackgroundColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color50"];
            switchequipmentlabel.TextColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["Color950"];
            horsesview.IsVisible = true;
            equipmentview.IsVisible = false;
        }
    }
    public void Buyhorses(object sender, EventArgs e)
    {
        if(sender is Button button)
        {
            GameData gameData = new GameData();
            gameData.updateMoney();
            int idcavalo= int.Parse(button.ClassId);
            Horse horse = Info.Horses.Single(x=>x.Id == idcavalo);
            if(Info.Money>=horse.BuyPrice && Info.HorsesSpaces> Info.Horses.Count(x => x.IsSold == true))
            {
                Info.Money -= horse.BuyPrice;
                horse.IsSold = true;
                OnAppearing();
            }
        }
    }
    public void BuyEquipment(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            int idequipment = int.Parse(button.ClassId);
            Equipment equipment = Info.Equipments.Single(x => x.Id == idequipment);
            if (Info.Money >= equipment.Price)
            {
                Info.Money -= equipment.Price;
                equipment.SoldAmount += 1;
                OnAppearing();
            }
        }
    }

    public void BuySpaces(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            int price = Info.HorsesSpacesPrice;
            if (Info.Money < price)
                return;
            Info.Money -= price;
            Info.HorsesSpaces += 5;
            Info.HorsesSpacesPrice = price * 2;

            BuySpacesButton.Text = $"Comprar +5 - {Info.HorsesSpacesPrice}€";
            horsesamount.Text = $"{Info.Horses.Count(x => x.IsSold == true).ToString()}/{Info.HorsesSpaces}";
        }
    }
}