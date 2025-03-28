namespace HorsesEmpire;

public partial class Horses : ContentPage
{
	public Horses()
	{
		InitializeComponent();


		//buscar itens
        //collectionViewPessoas.ItemsSource = app.Pessoas;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        horsesview.ItemsSource = Info.Horses.Where(x => x.IsSold == true).ToList();

    }
}