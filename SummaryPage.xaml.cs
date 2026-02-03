namespace MathQuiz;

using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MathQuiz.Services;
using SkiaSharp;

public class PieData(string name, double value, SolidColorPaint paint, string label)
{
	public string Name { get; set; } = name;
	public double[] Values { get; set; } = [value];
	public SolidColorPaint Fill { get; set; } = paint;
	public Func<ChartPoint, string> Formatter { get; set; } =
		point =>
		{
			var pv = point.Coordinate.PrimaryValue;
			var sv = point.StackedValue!;

			return $"{label}: {pv}/{sv.Total}{Environment.NewLine}{sv.Share:P0}";
		};
}

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
		PieChart.BindingContext = this;
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

	private void OnEnd(object sender, EventArgs e)
	{
		Application.Current.MainPage = new NavigationPage(new MainPage());
	}

	public PieData[] Data { get; set; } = [

	new ("Prawidłowe", AppState.CorrectAnswers, new SolidColorPaint(SKColors.Green),"Poprawne"),
		new ("Błędne", AppState.QuestionCount - AppState.CorrectAnswers, new SolidColorPaint(SKColors.Red), "Złe")
	];

}