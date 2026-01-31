using SQLite;
using MathQuiz.Models;

namespace MathQuiz.Services
{
    public class DatabaseService
    {
    internal SQLiteAsyncConnection db;

    public async Task Init()
    {
        if (db != null) return;
        var path = Path.Combine(FileSystem.AppDataDirectory, "results2.db");
        db = new SQLiteAsyncConnection(path);
        await db.CreateTableAsync<ResultHistory>();
    }

    public async Task AddResult(string operation, int correct, int total)
    {
        await Init();
        await db.InsertAsync(new ResultHistory
        {
            QuizStartDate = AppState.QuizStartTime,
			Operation = operation,
            Correct = correct,
            Total = total,
		});
    }

    public async Task<List<ResultHistory>> GetLast10Results()
    {
        await Init();
        return await db.Table<ResultHistory>().OrderByDescending(r => r.QuizStartDate).Take(10).ToListAsync();
    }
}
}