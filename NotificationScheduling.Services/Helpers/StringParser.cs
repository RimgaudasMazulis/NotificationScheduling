using NotificationScheduling.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace NotificationScheduling.Services.Helpers
{
    public static class StringParser
    {
        public static IEnumerable<int> CommaSeparatedToNumbers(this string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    var stringParts = text.Split(',').ToArray();
                    return stringParts.Select(sp => sp.TryParseIntoInt()).ToArray();
                }

                return null;
            }
            catch (TryParseException)
            {
                throw;
            }            
        }

        private static int TryParseIntoInt(this string text)
        {
            if (int.TryParse(text, out int number))
            {
                return number;
            }

            throw new TryParseException($"Could not parse {text} to number");
        }
    }
}
