namespace D3.Tests.Models.Queryable
{
    using System;
    using System.Collections.Generic;

    public class TestItemMapped
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<string> Children { get; set; }
    }
}