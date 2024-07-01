using FindPreviousSqrt;


namespace TestTask;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.That<int>(Program.MySqrt(8), Is.EqualTo(2));
        Assert.That<int>(Program.MySqrt(0), Is.EqualTo(0));
        Assert.That<int>(Program.MySqrt(4), Is.EqualTo(2));
    }
}