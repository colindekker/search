namespace D3.Tests.Core.Extensions
{
    using D3.Core.Extensions;
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void Capitalize_Null_String()
        {
            const string input = null;
            var result = input.Capitalize();
            Assert.Null(result);
        }

        [Fact]
        public void Capitalize_Empty_String()
        {
            const string input = "";
            var result = input.Capitalize();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Capitalize_Non_Empty_Word_String()
        {
            const string input = "colin";
            var result = input.Capitalize();
            Assert.Equal("Colin", result);
        }

        [Fact]
        public void Capitalize_Non_Empty_Alphanumeric_String()
        {
            const string input = "1colin";
            var result = input.Capitalize();
            Assert.Equal("1colin", result);
        }
    }
}
