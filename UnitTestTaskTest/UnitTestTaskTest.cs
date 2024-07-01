using UnitTestTask;

namespace UnitTestTaskTest;

public class UnitTestTaskTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.AreEqual(Program.MySqrt(8), 1);
    }
}