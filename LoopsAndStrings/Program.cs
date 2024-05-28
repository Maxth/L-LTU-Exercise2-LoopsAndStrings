// See https://aka.ms/new-console-template for more information


using System.Text;

while (true)
{
    Log("\nHello! You will navigate through this program by entering different integers.");
    Log("0. Exit");
    Log("1. Going to the cinema - what's the price?");
    Log("2. Going to the cinema - what's the price for the whole group?");
    Log("3. Repeat input 10 times");
    Log("4. What's the third word in the sentence?");

    string input = AskForString();

    switch (input)
    {
        case "0":
            Environment.Exit(0);
            break;
        case "1":
            CheckCinemaPrice("Enter the visitor's age please.", shouldLogPrice: true);
            break;
        case "2":
            CalculateAndPrintGroupPrice();
            break;
        case "3":
            RepeatInputTenTimes();
            break;
        case "4":
            PrintThirdWordInSentence();
            break;
        default:
            PrintInvalidInputFeedback();
            break;
    }
}

void PrintThirdWordInSentence()
{
    while (true)
    {
        string input = AskForString("Enter a sentence of at least three words please:");

        //To accomodate for several whitespaces in a row,
        //we use the Split method's option to remove all empty entries in the resulting array.
        //For further reference see https://learn.microsoft.com/en-us/dotnet/api/system.stringsplitoptions?view=net-8.0&redirectedfrom=MSDN
        string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (words.Length > 2)
        {
            Log($"The third word in that sentence is: {words[2]}");
            break;
        }
        else
        {
            PrintInvalidInputFeedback("You need to enter a sentence of at least three words!");
        }
    }
}

void RepeatInputTenTimes()
{
    string input = AskForString("Enter something to be repeated 10 times:");

    StringBuilder sb = new StringBuilder();
    for (int i = 0; i < 10; i++)
    {
        sb.Append($" {input}");
    }
    Log(sb.ToString());
}

void CalculateAndPrintGroupPrice()
{
    uint totalPrice = 0;
    while (true)
    {
        uint groupSize = AskForUint("How many persons in the group?");

        {
            if (groupSize < 1)
            {
                PrintInvalidInputFeedback("You need to enter a number larger than zero!");
                continue;
            }
            for (int i = 0; i < groupSize; i++)
            {
                totalPrice += CheckCinemaPrice($"Enter the age of person {i + 1}");
            }
        }
        Log($"Number of people in the group: {groupSize}");
        Log($"Cost for the whole group: {totalPrice}");
        break;
    }
}

// We use the bool shouldLogPrice to indicate if we should print out the price to the console (as requested in option 1),
//or just return the ticket price in int (as we do in option 2). This way we can use the method for both options.
//Also we pass the query into the method in order to make it suitable for the different options' circumstances.
uint CheckCinemaPrice(string query, bool shouldLogPrice = false)
{
    uint age = AskForUint(query);

    if (age < 5)
    {
        if (shouldLogPrice)
        {
            Log("Child price: free");
        }
        return 0;
    }
    if (age < 20)
    {
        if (shouldLogPrice)
        {
            Log("Youth price: 80 kr");
        }
        return 80;
    }
    if (age < 65)
    {
        if (shouldLogPrice)
        {
            Log("Standard price: 120 kr");
        }
        return 120;
    }
    if (age < 101)
    {
        if (shouldLogPrice)
        {
            Log("Retiree price: 90 kr");
        }
        return 90;
    }
    if (shouldLogPrice)
    {
        Log("Elder price: free");
    }
    return 0;
}

string AskForString(string prompt = "")
{
    while (true)
    {
        Log(prompt);
        string? input = Console.ReadLine();
        if (!String.IsNullOrWhiteSpace(input) && input.Length > 0)
        {
            return input;
        }
        else
        {
            PrintInvalidInputFeedback();
        }
    }
}

uint AskForUint(string prompt)
{
    while (true)
    {
        string input = AskForString(prompt);

        if (uint.TryParse(input, out uint result))
        {
            return result;
        }
        else
        {
            PrintInvalidInputFeedback("You need to enter a positive number!");
        }
    }
}

void PrintInvalidInputFeedback(string feedback = "")
{
    Log($"Invalid input.{feedback} Please try again!\n");
}

//We isolate this into a separate method,
//in case we later want to do something else,
//like sending the string to a UI
void Log(string output)
{
    Console.WriteLine(output);
}
