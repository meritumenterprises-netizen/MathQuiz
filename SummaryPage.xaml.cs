namespace MathQuiz;

using MathQuiz.Services;

public partial class SummaryPage : ContentPage
{
    public SummaryPage()
    {
        InitializeComponent();
        DatabaseService db = new();
        int wrong = AppState.QuestionCount - AppState.CorrectAnswers;
        double percentGood = (double)AppState.CorrectAnswers / AppState.QuestionCount * 100;
        double percentBad = 100 - percentGood;

        SummaryLabel.Text = $"Poprawne: {AppState.CorrectAnswers} ({percentGood:F0}%)\n\n" +
                            $"Błędne: {wrong} ({percentBad:F0}%)";
    }

    private void OnEnd (object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}