namespace SIQbic.API.Model
{
    public class QuestionResponse
    {
        public int Id { get; set; }

        public Question Question { get; set; }

        public string Response { get; set; }
    }
}