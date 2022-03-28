namespace JWTRestApi.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
        public string? Section { get; set; }

        public Guid? Guid { get; set; }
    }
}
