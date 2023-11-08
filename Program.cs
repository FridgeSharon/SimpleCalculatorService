
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
WebApplication app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapGet("/", () => "Server side by Guy Sharon, heyguysha@gmail.com");

app.MapPost("/calculate", Calculation());

app.Run();

static float Calculate(char currentOperator, float left, float right)
{
    return currentOperator switch
    {
        'x' => left * right,
        '/' => left / right,
        '+' => left + right,
        '-' => left - right,
        _ => 0,
    };
}

static Func<CalculationRequest, IResult> Calculation()
{
    return ([FromBody] CalculationRequest request) =>
    {
        // validateInput() is not implemented, task parameters state that Input is to be considered valid.
        string input = request.Input;
        List<float> numbers = new();
        List<char> operators = new();
        List<char> validOperators = new() { 'x', '/', '+', '-' };
        bool isNegative = false;

        // handle negative numbers
        if (input[0] == '-')
        {
            isNegative = true;
        }

        // Separate numbers and operations
        string currentNumber = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c) || c == '.')
            {
                currentNumber += c;
                if (isNegative)
                {
                    currentNumber = '-' + currentNumber;
                    isNegative = false;
                }
            }
            else if (!isNegative)
            {
                numbers.Add(float.Parse(currentNumber));
                currentNumber = "";
                operators.Add(c);
            }
        }
        numbers.Add(float.Parse(currentNumber));

        // Calculate
        while (operators.Count > 0)
        {
            int index = -1;
            foreach (char operation in validOperators)
            {
                index = operators.IndexOf(operation);
                if (index != -1)
                {
                    break;
                }
            }

            float left = numbers[index];
            float right = numbers[index + 1];
            char currentOperator = operators[index];
            float result = Calculate(currentOperator, left, right);
            numbers[index] = result;
            numbers.RemoveAt(index + 1);
            operators.RemoveAt(index);
        }
        float answer = numbers[0];

        return Results.Ok(answer);
    };
}


public class CalculationRequest
{
    public required string Input { get; set; }
}