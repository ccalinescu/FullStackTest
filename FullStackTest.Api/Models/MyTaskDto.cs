namespace FullStackTest.Api.Models
{
    public class MyTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }

        public string Status { 
            get { return Completed ? "Completed" : "To do"; } 
        }
    }
}
