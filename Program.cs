using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{    
    static void Main()
    {
        Console.WriteLine(convertToInt("MCMLXXIV"));
        Console.ReadKey();
    }

    private static int convertToInt(string romanNumeral)
    {
        var intvalues = validateAndConvertToValues(romanNumeral);

        // Ok iterate through the list of character values
        var total = 0;
        var prevVal = 0;
       
        foreach (int currVal in intvalues)
        {
            if (prevVal < currVal)
            {
                total = total - (prevVal * 2);  // x2 as we already added it
            }

            total = total + currVal;
            prevVal = currVal;
        }

        return total;
    }

    private static IList<int> validateAndConvertToValues(string romanNumeral)
    {
        // Check we have a numeral
        if (string.IsNullOrWhiteSpace(romanNumeral))
        {
            throw new ArgumentNullException();
        }

        // Check for invalid combinations of characters
        if (getInvalidCombinations().Any(invComb => romanNumeral.Contains(invComb)))
        {
            throw new ArgumentException("Invalid characters combination found in input numeral");
        }

        // Convert to list of int values
        var allowedValues = getAllowedValues();
        var intvalues = romanNumeral
        .ToUpper()
        .Where(c => allowedValues.ContainsKey(c))
        .Select(s => allowedValues[s])
        .ToList();

        // Check all characters were able to be converted
        if (intvalues.Count != romanNumeral.Length)
        {
            throw new ArgumentException("Invalid character found in input numeral");
        }

        return intvalues;
    }

    private static IList<string> getInvalidCombinations()
    {
        return new List<string>()
        {
            "IIII",
            "IL",
            "IC",
            "ID",
            "IM",
            "VV",
            "VX",
            "VL",
            "VC",
            "VD",
            "VM",
            "XXXX",
            "XD",
            "XM",
            "LL",
            "LC",
            "LD",
            "LM",
            "CCCC",
            "DD",
            "DM",
            "MMMM"
        };
    }

    private static Dictionary<char, int> getAllowedValues()
    {
        return new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };
    }
}