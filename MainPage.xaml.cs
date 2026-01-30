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
            var results = await db.GetLastFiveResults();
            var displayResults = results.Select(r => new
            {
                r.Operation,
                r.Correct,
                TotalMinusCorrect = r.Total - r.Correct,
                Percent = (double)r.Correct / r.Total * 100
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
}