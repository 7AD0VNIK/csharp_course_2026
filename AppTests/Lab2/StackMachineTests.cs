using App.Lab2;

namespace AppTests.Lab2;

public class StackMachineTests
{
    [TestCase(new[] { "push Привет! Это снова я! Пока!","pop 5","push Как твои успехи? Плохо?","push qwertyuiop","push 1234567890","pop 27" }, "Привет! Это снова я! Как твои успехи?")]
    [TestCase(new[] { "push Hello,", "push World!" }, "Hello,World!")]
    [TestCase(new[] { "push abc", "pop 3" }, "")]
    [TestCase(new[] { "push abc", "pop 0", "push def" }, "abcdef")]
    public void StackTests(string[] codeLines, string expected)
    {
        var result = StackMachine.CalculateString(codeLines);
        Assert.That(result, Is.EqualTo(expected));
    }
}