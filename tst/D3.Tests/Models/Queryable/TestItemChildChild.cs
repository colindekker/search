namespace D3.Tests.Models.Queryable
{
    using System;

    public class TestItemChildChild
        : IInterface2
    {
        public int Id { get; set; }

        public Guid Identifier { get; set; }

        public Guid? ReferenceIdentifier { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime? InvoiceOn { get; set; }

        public DateTime? InvoiceNext { get; set; }

        public double Cost { get; set; }

        public double? Sale { get; set; }

        public decimal CostPrice { get; set; }

        public decimal? SalePrice { get; set; }

        public TestItemChildChild Child { get; set; }
    }
}