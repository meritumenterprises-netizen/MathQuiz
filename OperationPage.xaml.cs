namespace MathQuiz;

public partial class OperationPage : ContentPage
{
    public OperationPage() => InitializeComponent();

    async void Select(char op)
    {
        AppState.Operation = op.ToString();
        await Navigation.PushAsync(new RangePage());
    }

    // Add these event handlers to your code-behind file
    private void OnAdditionClicked(object sender, EventArgs e)
    {
        Select('+');
    }

    private void OnSubtractionClicked(object sender, EventArgs e)
    {
        Select('-');
    }

    private void OnMultiplicationClicked(object sender, EventArgs e)
    {
        Select('*');
    }

    private void OnDivisionClicked(object sender, EventArgs e)
    {
        Select('/');
    }
}
