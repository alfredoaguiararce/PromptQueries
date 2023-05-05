using Queries.Ai.Models;

namespace Queries.Ai.Utils;
public class LinqQueryParser
{

    // Method to parse a LINQ query and extract its component parts
    public LinqQueryResultDTO ParseLinqQuery(string linqQuery)
    {
        // Create a new result object to store the extracted parts
        var result = new LinqQueryResultDTO();

        // Define an array of LINQ methods that we're interested in
        var methods = new[]
        {
                LinqMethods.Where,
                LinqMethods.Select,
                LinqMethods.OrderBy,
                LinqMethods.OrderByDescending,
                LinqMethods.ThenBy,
                LinqMethods.ThenByDescending,
                LinqMethods.Skip,
                LinqMethods.Take,
                LinqMethods.Distinct,
                LinqMethods.Reverse
            };

        // Loop over each of the methods in our array
        foreach (var method in methods)
        {
            // Find the starting index of the current method in the query string
            var indexOfMethod = linqQuery.IndexOf(method);
            if (indexOfMethod == -1)
            {
                // If the method isn't found, skip to the next one
                continue;
            }

            // Extract the substring of the query that follows the method
            var substring = linqQuery.Substring(indexOfMethod + method.Length);

            // Find the index of the first open bracket after the method
            var firstOpenBracketIndex = substring.IndexOf("(");
            if (firstOpenBracketIndex == -1)
            {
                // If no open bracket is found, skip to the next method
                continue;
            }

            // Extract the substring that follows the open bracket
            var remainingSubstring = substring.Substring(firstOpenBracketIndex);

            // Use a stack to keep track of nested brackets
            var stack = new Stack<char>();
            stack.Push('(');

            var i = 1;
            while (stack.Count > 0 && i < remainingSubstring.Length)
            {
                var currentChar = remainingSubstring[i];
                switch (currentChar)
                {
                    // If we encounter an open bracket, push it onto the stack
                    case '(':
                        stack.Push(currentChar);
                        break;

                    // If we encounter a close bracket, pop the most recent open bracket from the stack
                    case ')':
                        stack.Pop();
                        break;
                }

                i++;
            }

            // Extract the lambda expression that's enclosed in the brackets
            var lambdaExpression = remainingSubstring.Substring(1, i - 2);

            // Store the lambda expression in the appropriate property of the result object
            switch (method)
            {
                case LinqMethods.Where:
                    result.WhereLambda = lambdaExpression;
                    break;

                case LinqMethods.Select:
                    result.SelectLambda = lambdaExpression;
                    break;

                case LinqMethods.OrderBy:
                    result.OrderByLambda = lambdaExpression;
                    break;

                case LinqMethods.OrderByDescending:
                    result.OrderByDescendingLambda = lambdaExpression;
                    break;

                case LinqMethods.ThenBy:
                    result.ThenByLambda = lambdaExpression;
                    break;

                case LinqMethods.ThenByDescending:
                    result.ThenByDescendingLambda = lambdaExpression;
                    break;

                case LinqMethods.Skip:
                    result.SkipLambda = lambdaExpression;
                    break;

                case LinqMethods.Take:
                    result.TakeLambda = lambdaExpression;
                    break;

                case LinqMethods.Distinct:
                    result.DistinctLambda = lambdaExpression;
                    break;

                case LinqMethods.Reverse:
                    result.ReverseLambda = lambdaExpression;
                    break;

                case LinqMethods.Average:
                    result.AverageLambda = lambdaExpression;
                    break;
            }
        }

        // Return the result object with all of the extracted parts
        return result;
    }
}