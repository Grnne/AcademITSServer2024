using FindPreviousSqrt;
using VectorTask;


namespace TestTask;

public class Tests
{
    [Test]
    public void FindPreviousSqrtTest()
    {
        Assert.That<int>(Program.MySqrt(8), Is.EqualTo(2));
        Assert.That<int>(Program.MySqrt(0), Is.EqualTo(0));
        Assert.That<int>(Program.MySqrt(4), Is.EqualTo(2));

    }

    [Test]
    public void VectorTaskTestReverse()
    {
        var vector1 = new Vector([0, 0, 0, 1, 3]);
        vector1.Reverse();
        var expected1 = new Vector([-0, -0, -0, -1, -3]);

        Assert.That(vector1, Is.EqualTo(expected1));

        var vector2 = new Vector([0]);
        vector2.Reverse();

        var expected2 = new Vector([-0]);

        Assert.That(vector2, Is.EqualTo(expected2));
    }

    [Test]
    public void VectorTaskTestSubtract()
    {
        var vector1 = new Vector([0, 0, 0, 1, 3]);
        var vector2 = new Vector([1, 1, 1, 1, 1, 1, 1]);

        vector1.Subtract(vector2);

        var expected = new Vector([-1,-1,-1,0,2,-1,-1]);

        Assert.That(vector1, Is.EqualTo(expected));
    }
}