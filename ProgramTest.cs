using Xunit;

public class ProgramTest
{
    [Fact]
    public void TestCalculate_Addition()
    {
        // Arrange
        char op = '+';
        float left = 2.5f;
        float right = 3.5f;

        // Act
        float result = Program.Calculate(op, left, right);

        // Assert
        Assert.Equal(6.0f, result);
    }

    [Fact]
    public void TestCalculate_Subtraction()
    {
        // Arrange
        char op = '-';
        float left = 5.0f;
        float right = 2.5f;

        // Act
        float result = Program.Calculate(op, left, right);

        // Assert
        Assert.Equal(2.5f, result);
    }

    [Fact]
    public void TestCalculate_Multiplication()
    {
        // Arrange
        char op = '*';
        float left = 2.0f;
        float right = 3.0f;

        // Act
        float result = Program.Calculate(op, left, right);

        // Assert
        Assert.Equal(6.0f, result);
    }

    [Fact]
    public void TestCalculate_Division()
    {
        // Arrange
        char op = '/';
        float left = 10.0f;
        float right = 2.0f;

        // Act
        float result = Program.Calculate(op, left, right);

        // Assert
        Assert.Equal(5.0f, result);
    }
}
