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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            if (Finish?.IsEnabled == true && Finish.IsVisible)
            {
                Finish.Focus();
            }
        });
    }

    private void OnEnd (object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}