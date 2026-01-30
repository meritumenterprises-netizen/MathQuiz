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
        var path = Path.Combine(FileSystem.AppDataDirectory, "results.db");
        db = new SQLiteAsyncConnection(path);
        await db.CreateTableAsync<ResultHistory>();
    }

    public async Task AddResult(string operation, int correct, int total)
    {
        await Init();
        await db.InsertAsync(new ResultHistory
        {
            Operation = operation,
            Correct = correct,
            Total = total
        });
    }

    public async Task<List<ResultHistory>> GetLastFiveResults()
    {
        await Init();
        return await db.Table<ResultHistory>().OrderByDescending(r => r.Id).Take(5).ToListAsync();
    }
}
}