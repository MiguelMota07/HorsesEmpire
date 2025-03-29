
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
        if (Info.Equipment == null)
        {
            return;
        }
        var expandedEquipmentList = new List<Equipment>();

        foreach (var equipment in Info.Equipment.Where(x => x.SoldAmount > x.InUse))
        {
            int availableCount = equipment.SoldAmount - equipment.InUse;

            for (int i = 0; i < availableCount; i++)
            {
                expandedEquipmentList.Add(equipment);
            }
        }

        equipmentview.ItemsSource = expandedEquipmentList;

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
    private string equipmenttochange = "";
    public async void ChangeEquipment(object sender, EventArgs e)
    {

        string[] ids = equipmenttochange.Split(',');
        if(sender is Border border)
        {
            if (border.ClassId.Contains(','))
            {
                if (equipmenttochange == "")
                {
                    Horsetoolpopup.IsVisible = true;
                    equipmenttochange = border.ClassId;
                }
                else if (equipmenttochange == border.ClassId)
                {
                    equipmenttochange = "";
                    Horsetoolpopup.IsVisible = false;
                }
                else if (equipmenttochange != border.ClassId)
                {
                    equipmenttochange = border.ClassId;
                    Horsetoolpopup.IsVisible = false;
                    await Task.Delay(70);
                    Horsetoolpopup.IsVisible = true;
                }
            }
            else
            {

            }
        }

    }
}