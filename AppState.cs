// =========================
// AppState.cs – wspólny stan aplikacji
// =========================
namespace MathQuiz;

public static class AppState
{
    public static string Operation = "+";
    public static int MaxRange = 30;
    public static int QuestionCount = 10;

    public static int CurrentQuestion = 0;
    public static int CorrectAnswers = 0;

    public static int A, B;
    public static int UserAnswer;
}
