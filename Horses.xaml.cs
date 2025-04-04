
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
        GameData gameData = new GameData();
        gameData.updateMoney();
        horsesview.ItemsSource = Info.Horses.Where(x => x.IsSold == true).ToList();
        if (Info.Equipments == null)
        {
            return;
        }
        var expandedEquipmentList = new List<Equipment>();

        foreach (var equipment in Info.Equipments.Where(x => x.SoldAmount > x.InUse))
        {
            int availableCount = equipment.SoldAmount - equipment.InUse;

            for (int i = 0; i < availableCount; i++)
            {
                expandedEquipmentList.Add(equipment);
            }
        }

        equipmentview.ItemsSource = expandedEquipmentList.AsEnumerable().Reverse().ToList();
        int numHorses= Info.Horses.Count(x=>x.IsSold==true);
        horsesamount.Text=$"{numHorses.ToString()}/{Info.HorsesSpaces}";

    }
    /*
    public static double FindMultiplier(int id)
    {
        return Info.Equipment.Single(x => x.Id == id).Multiplier;
    }*/
    public void Sellhorse(object sender, EventArgs e)
    {
        if(sender is Button button )  
        {
            GameData gameData = new GameData();
            gameData.updateMoney();
            int id =int.Parse(button.ClassId);
            Horse horse = Info.Horses.Single(x => x.Id == id);
            horse.IsSold = false;
            Horsetoolpopup.IsVisible = false;
            Info.Money += horse.SellPrice;
            foreach(Equipment equipment in horse.Equipments)
            {
                equipment.InUse -=1;
            }
            horse.Equipments.Clear();
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
            else if (border.ClassId.Contains('x'))
            {
                Horse horse = Info.Horses.Single(x => x.Id == int.Parse(ids[0]));
                if(horse.Equipments.Count> int.Parse(ids[1]) - 1)
                {
                horse.Equipments[int.Parse(ids[1]) - 1].InUse -= 1;
                horse.Equipments.RemoveAt(int.Parse(ids[1])-1);
                equipmenttochange = "";
                Horsetoolpopup.IsVisible = false;
                OnAppearing();
                }
                
            }
            else
            {
                GameData gameData = new GameData();
                gameData.updateMoney();

                int idnovoequipamento = int.Parse(border.ClassId);
                Equipment equipment = Info.Equipments.Single(x => x.Id == idnovoequipamento);
                Horse horse = Info.Horses.Single(x => x.Id == int.Parse(ids[0]));
                if (horse != null)
                {
                    equipment.InUse += 1;

                    if (horse.Equipments.Count > int.Parse(ids[1]) - 1)
                    {
                        horse.Equipments[int.Parse(ids[1]) - 1].InUse -= 1;
                        horse.Equipments[int.Parse(ids[1]) - 1] = equipment;
                    }
                    else
                    {
                        horse.Equipments.Add(equipment);
                    }
                }
                Horsetoolpopup.IsVisible = false;
                equipmenttochange = "";


                OnAppearing();
            }
        }

    }
}