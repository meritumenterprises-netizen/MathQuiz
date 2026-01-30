namespace MathQuiz;

public partial class RangePage : ContentPage
{
    public RangePage() => InitializeComponent();

    async void SetRange(int range)
    {
        AppState.MaxRange = range;
        await Navigation.PushAsync(new CountPage());
    }

    private void Do30_Clicked(object sender, EventArgs e)
    {
        SetRange(30);
    }

    private void Do50_Clicked(object sender, EventArgs e)
    {
        SetRange(50);
    }

    private void Do100_Clicked(object sender, EventArgs e)
    {
        SetRange(100);
    }
}