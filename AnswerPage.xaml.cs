namespace MathQuiz;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using MathQuiz.Services;

public partial class AnswerPage : ContentPage
{
    DatabaseService db = new();
    public AnswerPage()
    {
        InitializeComponent();
        ProgressBar.Progress = (double)(AppState.CurrentQuestion + 1) / AppState.QuestionCount;

        int correct = AppState.Operation switch
        {
            "+" => AppState.A + AppState.B,
            "-" => AppState.A - AppState.B,
            "*" => AppState.A * AppState.B,
            "/" => AppState.A / AppState.B,
            _ => 0
        };

        if (AppState.UserAnswer == correct)
        {
            AppState.CorrectAnswers++;
            ResultLabel.Text = "Poprawna odpowiedź";
            ResultLabel.Text = "Poprawna odpowiedź";
            ResultIcon.Text = "✔"; // zielony check
            ResultIcon.TextColor = Colors.Green;
            HapticFeedback.Perform(HapticFeedbackType.Click);
        }
        else
        {
            ResultLabel.Text = $"Błędna odpowiedź. Poprawna: {correct}";
            ResultIcon.Text = "✖"; // czerwony x
            ResultIcon.TextColor = Colors.Red;
            try
            {
                Vibration.Vibrate(TimeSpan.FromMilliseconds(500));
            }
            catch (Exception) { /* brak wsparcia wibracji */ }
        }
        QuestionProgress.Text = $"Pytanie {AppState.CurrentQuestion + 1} z {AppState.QuestionCount}";
        UpdateCurrentResultInDb();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            if (Next?.IsEnabled == true && Next.IsVisible)
            {
                Next.Focus();
            }
        });
    }


    private async void UpdateCurrentResultInDb()
    {
        await db.Init();
        // usuń poprzedni wpis dla bieżącego quizu, jeśli istnieje
        var existing = await db.db.Table<MathQuiz.Models.ResultHistory>()
                        .Where(r => r.Operation == AppState.Operation && r.Total == AppState.QuestionCount)
                        .OrderByDescending(r => r.Id).FirstOrDefaultAsync();
        if (existing != null)
        {
            existing.Correct = AppState.CorrectAnswers;
            await db.db.UpdateAsync(existing);
        }
        else
        {
            await db.AddResult(AppState.Operation, AppState.CorrectAnswers, AppState.QuestionCount);
        }
    }


    async void OnNext(object sender, EventArgs e)
    {
        AppState.CurrentQuestion++;
        if (AppState.CurrentQuestion >= AppState.QuestionCount)
            await Navigation.PushAsync(new SummaryPage());
        else
            await Navigation.PushAsync(new QuestionPage());
    }

    async void OnFinishClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SummaryPage());
    }
}