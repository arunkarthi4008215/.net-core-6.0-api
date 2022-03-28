namespace JWTRestApi.Models
{
    public partial class Studentresult
    {
        public int Id { get; set; }
        public int Semmester { get; set; }
        public string? Subject { get; set; }

        public int Mark { get; set; }

        public int StudentID { get; set; }
    }
}
