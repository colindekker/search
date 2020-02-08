namespace D3.Tests.Models.Entities
{
    using System.Collections.Generic;

    public class Parent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Child> Children { get; set; }
    }
}
