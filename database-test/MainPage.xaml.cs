using database_test.Models;

namespace database_test;

public partial class MainPage : ContentPage
{

    
    
    public MainPage()
	{
		InitializeComponent();
        //ustawia guzik zmiany poziomu trudnosci odpowiednio, w zaleznosci od danych trzymanych w zmiennej globalnej
        SwitchDifficultyButton();
        
	}


    //w zaleznosci od tego jaki guzik zostal wcisniety (jaki rodzaj gry odpalony)
    //metoda odpalająca widok GamePage dostanie inne dane (zostanie poinformowana jaki rodzaj gry zostal wybrany)
    private void OnGameChosen(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        Navigation.PushAsync(new GamePage(btn.Text switch
        {
            "+" => "Addition",
            "-" => "Substraction",
            "*" => "Multiplication",
            "/" => "Division",
            _ => ""
        }));

    }

    //odpala widok z rejestrem poprzednich rozgrywek
    private void OnPreviousGameChosen(object sender, EventArgs e)
    {
        

        Navigation.PushAsync(new PreviousGames());

    }

    //po klilnieciu guzika ze zmiana poziomu trudnosci, zmienia sie on i informacje na ten temat sa trzymane w zmiennej globalnej
    private void OnDifficultyChanged(object sender, EventArgs e)
    {
        if (App.Difficulty == 2) App.Difficulty = 0;
        else App.Difficulty++;
        SwitchDifficultyButton();


    }

    //metoda ktora zmienia wyglad guzika do zmiany poziomu trudnosci
    private void SwitchDifficultyButton()
    {
        DifficultyButton.Text = "Difficulty: " + (DifficultyLevel)App.Difficulty;
        switch (App.Difficulty)
        {
            case 0:
                DifficultyButton.BackgroundColor = Color.FromRgb(50, 205, 50);
                break;
            case 1:
                DifficultyButton.BackgroundColor = Color.FromRgb(255, 215, 0);
                break;
            case 2:
                DifficultyButton.BackgroundColor = Color.FromRgb(255, 0, 0);
                break;

        }
    }

    




}

