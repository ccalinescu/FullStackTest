namespace FullStackTest.Api.Models
{
    public class MyTaskUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}
