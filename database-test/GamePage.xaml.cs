using database_test.Models;


namespace database_test;

public partial class GamePage : ContentPage
{
    public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	const int totalQuestions = 3;
	int gamesLeft = totalQuestions;
	string difficulty = "" + (DifficultyLevel)App.Difficulty;
	public GamePage(string gameType)
	{
		// przyjmuje dane na temat typu rozgrywanej gry oraz ustawia widocznosc poszczegolnych komponentow 
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;
		GameOverArea.IsVisible = false;
		QuestionArea.IsVisible = true;

		CreateNewQuestion();
	}

    //generuje nowe pytanie do gry
    private void CreateNewQuestion()
	{
		
		var gameOperation = GameType switch
		{
			"Addition" => "+",
			"Substraction" => "-",
			"Multiplication" => "*",
			"Division" => "/",
			_ => ""
		};

		var random = new Random();

		//gdy dzielenie to inny przedzial, druga liczba jest mniejsza od pierwszej i zawsze to sa takie wartosci zeby dzielenie bylo bez reszty
		firstNumber = GameType != "Division" ? random.Next(1, (int)Math.Pow(10, App.Difficulty+1)-1) : random.Next(1, (int)Math.Pow(10, App.Difficulty + 2) - 1);
        secondNumber = GameType != "Division" ? random.Next(1, (int)Math.Pow(10, App.Difficulty + 1) - 1) : random.Next(1, (int)Math.Pow(10, App.Difficulty + 2) - 1);

        if (GameType == "Division")
		{
			while (firstNumber < secondNumber || firstNumber % secondNumber != 0)
			{
				firstNumber = random.Next(1, (int)Math.Pow(10, App.Difficulty + 2));
				secondNumber = random.Next(1, (int)Math.Pow(10, App.Difficulty + 2));
			}
		}
		
		QuestionLabel.Text = $"{firstNumber} {gameOperation} {secondNumber}";
    }

	//metoda do wysylania odpowiedz
	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		//przyjmuje tylko wartosci liczbowe, gdy nie sa liczbowe to gra ustawia wartosc na 0
		var answer = 0;
		try
		{
            answer = Int32.Parse(AnswerEntry.Text);
        }catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}


		
		var isCorrect = false;

		switch (GameType)
		{
			case "Addition":
				isCorrect = answer == firstNumber + secondNumber;
				break;
            case "Substraction":
                isCorrect = answer == firstNumber - secondNumber;
                break;
            case "Multiplication":
                isCorrect = answer == firstNumber * secondNumber;
                break;
            case "Division":
                isCorrect = answer == firstNumber / secondNumber;
                break;

        }
        ProcessAnswer(isCorrect);

		gamesLeft--;
		AnswerEntry.Text = "";

		if (gamesLeft > 0) 
			CreateNewQuestion();
		else 
			GameOver();
    }

	//metoda do zakonczenia gry, ustawia odpowiednie widocznosci, pokazuje wynik i wprowadza dane do bazy danych
    private void GameOver()
    {
        GameOperation gameOperation = GameType switch
		{
            "Addition" => GameOperation.Addition,
            "Substraction" => GameOperation.Substraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division
		};

		QuestionArea.IsVisible = false;
        GameOverArea.IsVisible = true;
        GameOverScoreLabel.Text = $"{score} out of {totalQuestions} correct";

		App.GameRepository.Add(new Game { 
		DatePlayed = DateTime.Now,
		Type = gameOperation,
		Score = score,
		Difficulty = difficulty});
    }

	//metoda do liczenia punktow
    private void ProcessAnswer(bool isCorrect)
    {
		if (isCorrect) score += 1;

		AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect";
    }

	//metoda do powrotu do poprzedniego widoku
	private void OnBackToMenu(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
}