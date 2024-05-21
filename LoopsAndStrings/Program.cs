// See https://aka.ms/new-console-template for more information



using System.Xml;

while (true)
{
    Log("\nHello! You will navigate through this program by entering different integers.");
    Log("0. Exit");
    Log("1. Going to the cinema - what's the price?");
    Log("2. Going to the cinema - what's the price for the whole group?");
    Log("3. Repeat input 10 times");
    Log("4. What's the third word in the sentence?");

    string? input = Console.ReadLine();

    switch (input)
    {
        case "0":
            Environment.Exit(0);
            break;
        case "1":
            CheckCinemaPrice(true, "Enter the visitor's age please.");
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
            PrintInvalidInputFeedback(null);
            break;
    }
}

void PrintThirdWordInSentence()
{
    while (true)
    {
        Log("Enter a sentence of at least three words please:");
        string? input = Console.ReadLine();

        //To accomodate for several whitespaces in a row,
        //we use the Split method's option to remove all empty entries in the resulting array.
        //For further reference see https://learn.microsoft.com/en-us/dotnet/api/system.stringsplitoptions?view=net-8.0&redirectedfrom=MSDN
        if (input != null && input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length > 2)
        {
            Log(
                $"The third word in that sentence is: {input.Split(" ", StringSplitOptions.RemoveEmptyEntries)[2]}"
            );
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
    while (true)
    {
        Log("Enter something to be repeated 10 times:");
        string? input = Console.ReadLine();

        if (input != null && input.Length > 0)
        {
            string output = "";
            for (int i = 0; i < 10; i++)
            {
                output += $" {input}";
            }

            Log(output);
            break;
        }
        else
        {
            PrintInvalidInputFeedback(null);
        }
    }
}

void CalculateAndPrintGroupPrice()
{
    int totalPrice = 0;
    while (true)
    {
        Log("How many persons in the group?");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int groupSize))
        {
            if (groupSize < 1)
            {
                PrintInvalidInputFeedback("You need to enter an integer larger than zero!");
                continue;
            }
            for (int i = 0; i < groupSize; i++)
            {
                totalPrice += CheckCinemaPrice(false, $"Enter the age of person {i + 1}");
            }
        }
        Log($"Number of people in the group: {groupSize}");
        Log($"Cost for the whole group: {totalPrice}");
        break;
    }
}

// We use the Boolean verbose to indicate if we should print out the price to the console (as requested in option 1),
//or just return the ticket price in int (as we do in option 2). This way we can use the method for both options.
//Also we pass the query into the method in order to make it suitable for the different options' circumstances.
int CheckCinemaPrice(Boolean verbose, string query)
{
    while (true)
    {
        Log(query);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int age))
        {
            if (age < 0)
            {
                PrintInvalidInputFeedback("Age cannot be negative!");
                continue;
            }
            if (age < 5)
            {
                if (verbose)
                {
                    Log("Child price: free");
                }
                return 0;
            }
            if (age < 20)
            {
                if (verbose)
                {
                    Log("Youth price: 80 kr");
                }
                return 80;
            }
            if (age < 65)
            {
                if (verbose)
                {
                    Log("Standard price: 120 kr");
                }
                return 120;
            }
            if (age < 101)
            {
                if (verbose)
                {
                    Log("Retiree price: 90 kr");
                }
                return 90;
            }
            if (verbose)
            {
                Log("Elder price: free");
            }
            return 0;
        }
        else
        {
            PrintInvalidInputFeedback("You need to enter a positive integer!");
        }
    }
}

void PrintInvalidInputFeedback(string? feedback)
{
    Log($"Invalid input. {feedback ?? ""} Please try again!\n");
}

void Log(string output)
{
    Console.WriteLine(output);
}
