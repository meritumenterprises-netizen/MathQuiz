using System.Timers;

namespace MathQuiz;

public partial class QuestionPage : ContentPage
{
    int time = 30;
    System.Timers.Timer timer;
    int remainingTime;

    public QuestionPage()
    {
        InitializeComponent();
        ProgressBar.Progress = (double)(AppState.CurrentQuestion + 1) / AppState.QuestionCount;
        QuestionProgress.Text = $"Pytanie {AppState.CurrentQuestion + 1} z {AppState.QuestionCount}";
        GenerateQuestion();
        remainingTime = time + 1;
        StartTimer();
        AnswerEntry.Completed += (s, e) => OnAnswer(s, e);
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () =>
        {
            if (AnswerEntry?.IsEnabled == true && AnswerEntry.IsVisible)
            {
                AnswerEntry.Focus();
            }
        });
    }

    void GenerateQuestion()
    {
        var rnd = new Random();
        
        switch (AppState.Operation)
        {
            case "+":
                {
                    AppState.A = rnd.Next(1, AppState.MaxRange);
                    var B = rnd.Next(1, AppState.MaxRange - AppState.A + 1); // Ensure A + B <= MaxRange
                    AppState.B = B;
                }
                break;
            case "-":
                {
                    AppState.A = rnd.Next(1, AppState.MaxRange + 1); // A can be any number up to MaxRange inclusive
                    var B = rnd.Next(1, AppState.A);
                    AppState.B = B;
                }
                break;
            case "*":
                {
                    AppState.A = rnd.Next(1, 11);
                    AppState.B = rnd.Next(1, AppState.MaxRange / AppState.A + 1); 
                }
                break;
            case "/":
                {
                    AppState.A = rnd.Next(1, AppState.MaxRange);
                    AppState.B = rnd.Next(1, AppState.MaxRange / 2 + 1); // Avoid too large divisors
                    AppState.A = AppState.B * rnd.Next(1, AppState.MaxRange / AppState.B + 1); // Ensure A is multiple of B
                }
                break;

        }
        QuestionLabel.Text = $"{AppState.A} {AppState.Operation} {AppState.B} = ";
        ProgressBarQuestion.Progress = 1;
    }

    void StartTimer()
    {
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += (s, e) =>
        {
            remainingTime--;
            double progress = (double)remainingTime / time;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = $"Pozostały czas: {remainingTime}s";
                ProgressBarQuestion.Progress = progress;
            });

            if (remainingTime <= 0)
            {
                timer.Stop();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new AnswerPage());
                });
            }
        };
        timer.Start();
    }

    async void OnAnswer(object sender, EventArgs e)
    {
        timer.Stop();
        AppState.UserAnswer = int.TryParse(AnswerEntry.Text, out var v) ? v : 0;
        await Navigation.PushAsync(new AnswerPage());
    }

    async void OnFinishClicked(object sender, EventArgs e)
    {
        timer?.Stop();
        await Navigation.PushAsync(new SummaryPage());
    }

    async Task GoNext()
    {
        await Navigation.PushAsync(new AnswerPage());
    }

}