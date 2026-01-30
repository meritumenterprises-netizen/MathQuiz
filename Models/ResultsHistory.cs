using SQLite;

namespace MathQuiz.Models
{
    public class ResultHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Operation { get; set; }
        public int Correct { get; set; }
        public int Total { get; set; }
    }
}