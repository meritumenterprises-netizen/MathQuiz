using System;
using Microsoft.Maui.Controls;

namespace MathQuiz
{
    public partial class CountPage : ContentPage
    {
        public CountPage()
        {
            InitializeComponent();
        }

        private void OnSetCount10(object sender, EventArgs e)
        {
            SetCount(10);
        }

        private void OnSetCount20(object sender, EventArgs e)
        {
            SetCount(20);
        }

        private void OnSetCount30(object sender, EventArgs e)
        {
            SetCount(30);
        }

        private void OnSetCount40(object sender, EventArgs e)
        {
            SetCount(40);
        }

        private void OnSetCount50(object sender, EventArgs e)
        {
            SetCount(50);
        }

        private void SetCount(int count)
        {
            AppState.QuestionCount = count;
            AppState.CurrentQuestion = 0;
            AppState.CorrectAnswers = 0;
            Navigation.PushAsync(new QuestionPage());
        }
    }
}