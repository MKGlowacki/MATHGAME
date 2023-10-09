namespace database_test;

public partial class PreviousGames : ContentPage
{
	
	public PreviousGames()
	{
		InitializeComponent();
        BindingContext = this; 

		//do wyswietlania danych na temat poprzednich rozgrywek
        gamesList.ItemsSource = App.GameRepository.GetAllGames() ;
		
	}

	private void OnDelete(object sender, EventArgs e)
	{
		Button button = (Button)sender;

		//do usuwania odpowieniego rejestru z db
        App.GameRepository.Delete((int)button.BindingContext);
		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}
}