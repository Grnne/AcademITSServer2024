using VectorTask;

namespace XUnitTestTask;

public class VectorTests
{
    [Fact]
    public void Reverse_ReversingVector()
    {
        var vector = new Vector([0, 0, 0, 1, 3]);
        var expected = new Vector([-0, -0, -0, -1, -3]);

        vector.Reverse();

        Assert.Equal(vector, expected);
    }

    [Fact]
    public void GetDifference_ReturnsVectorsDifference()
    {
        var vector1 = new Vector([0, 0, 0, 1, 3]);
        var vector2 = new Vector([1, 1, 1, 1, 1, 1, 1]);
        var expected = new Vector([-1, -1, -1, 0, 2, -1, -1]);

        var actual = Vector.GetDifference(vector1, vector2);

        Assert.Equal(actual, expected);
    }
}