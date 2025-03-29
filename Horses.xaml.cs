
namespace HorsesEmpire;

public partial class Horses : ContentPage
{
    
    public Horses()
	{
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        horsesview.ItemsSource = Info.Horses.Where(x => x.IsSold == true).ToList();

    }
    /*
    public static double FindMultiplier(int id)
    {
        return Info.Equipment.Single(x => x.Id == id).Multiplier;
    }*/
    public void Sellhorse(object sender, EventArgs e)
    {
        if(sender is Button button)
        {
            int id =int.Parse(button.ClassId);
            Horse horse = Info.Horses.Single(x => x.Id == id);
            horse.IsSold = false;
            OnAppearing();
        }
    }
    public void ChangeEquipment(object sender, EventArgs e)
    {

    }
}