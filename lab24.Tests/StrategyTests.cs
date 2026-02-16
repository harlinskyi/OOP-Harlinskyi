using Xunit;
using lab24.Strategies;

namespace lab24.Tests
{
    public class StrategyTests
    {
        [Theory]
        [InlineData(5, 25)]
        [InlineData(-3, 9)]
        [InlineData(0, 0)]
        public void SquareStrategy_ShouldCalculateCorrectValue(double input, double expected)
        {
            var strategy = new SquareOperationStrategy();
            var result = strategy.Execute(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 8)]
        [InlineData(-3, -27)]
        public void CubeStrategy_ShouldCalculateCorrectValue(double input, double expected)
        {
            var strategy = new CubeOperationStrategy();
            var result = strategy.Execute(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SquareRootStrategy_ShouldCalculateCorrectValue()
        {
            var strategy = new SquareRootOperationStrategy();
            Assert.Equal(4, strategy.Execute(16));
            Assert.Equal(3, strategy.Execute(9));
        }
    }
}