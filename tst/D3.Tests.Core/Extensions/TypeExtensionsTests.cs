namespace D3.Tests.Core.Extensions
{
    using D3.Core.Extensions;
    using D3.Tests.Models.Queryable;
    using Xunit;

    public class TypeExtensionsTests
    {
        [Fact]
        public void Check_Interface_Implements_Interface()
        {
            Assert.True(typeof(IInterface2).Implements<IInterface1>());
        }

        [Fact]
        public void Check_Type_Implements_Interface()
        {
            Assert.True(typeof(TestItem).Implements<IInterface1>());
        }

        [Fact]
        public void Check_Type_Implements_Interface_Nested()
        {
            Assert.True(typeof(TestItemChild).Implements<IInterface1>());
        }

        [Fact]
        public void Check_Type_ParentType_Implements_Interface()
        {
            Assert.True(typeof(TestItemDerived).Implements<IInterface1>());
        }

        [Fact]
        public void Check_Type_ParentType_Implements_Interface_Nested()
        {
            Assert.True(typeof(TestItemChildDerived).Implements<IInterface1>());
        }

        [Fact]
        public void Check_Type_Implements_Interface_Not_Implemented()
        {
            Assert.False(typeof(TestItem).Implements<IInterface3>());
        }
    }
}
