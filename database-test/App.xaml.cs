using database_test.Data;

namespace database_test;

public partial class App : Application
{

	// zmienne globalne do ustawienia poziomu trudnosci i laczenia z baza danych
	public static GameRepository GameRepository { get; private set; }
	public static int Difficulty { get; set; }
	public App(GameRepository gameRepository)
	{
		InitializeComponent();

		MainPage = new AppShell();

        GameRepository = gameRepository;


	}
}
