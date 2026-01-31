using Microsoft.Maui.Controls;

namespace MathQuiz;

using MathQuiz.Services;
using MathQuiz.Models;
using SQLite;

public partial class MainPage : ContentPage
{
    DatabaseService db = new();

    public MainPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var results = await db.GetLast10Results();
            var displayResults = results.Select(r => new
            {
                r.Operation,
                r.Correct,
                r.Total,
                TotalMinusCorrect = r.Total - r.Correct,
                Percent = (double)r.Correct / r.Total * 100,
                QuizStartTime = r.QuizStartDate.ToString("g"),
				StatusIcon = ((double)r.Correct / r.Total * 100) == 100 ? "✔" : "✖",
				StatusColor = ((double)r.Correct / r.Total * 100) == 100 ? Colors.Green : Colors.Red
			}).ToList();
            HistoryView.ItemsSource = displayResults;
        }
        catch (Exception ex)
        {
            //await DisplayAlert("Błąd", $"Nie można załadować historii wyników: {ex.Message}", "OK");
        }
    }

    async void OnStartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OperationPage());
    }

	private async void OnEmailTapped(object sender, EventArgs e)
	{
		await Launcher.OpenAsync(new Uri("mailto:pluczak99@gmail.com"));
	}
}