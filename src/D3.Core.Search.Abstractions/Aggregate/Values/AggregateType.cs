namespace D3.Core.Search.Aggregate.Values
{
    using D3.Core.Search.Aggregate.Models;

    public enum AggregateType
    {
        /// <summary>
        /// Sum the values of the <seealso cref="IAggregateColumn" />s included in an <see cref="IAggregateConfig{TColumn}"/>.
        /// </summary>
        Sum,

        /// <summary>
        /// Average the values of the <seealso cref="IAggregateColumn" />s included in an <see cref="IAggregateConfig{TColumn}"/>.
        /// </summary>
        Average,

        /// <summary>
        /// Return the difference between the left and right column values of the <seealso cref="IAggregateColumn" />s included in an <see cref="IAggregateConfig{TColumn}"/>.
        /// </summary>
        Difference,

        /// <summary>
        /// Return values of the <seealso cref="IAggregateColumn" />s included in an <see cref="IAggregateConfig{TColumn}"/> as a percentage.
        /// </summary>
        Percentage
    }
}
