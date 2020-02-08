namespace D3.Tests.Models.Entities
{
    public class Child
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public Parent Parent { get; set; }
    }
}
