using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MySecondHand.Spider.Model
{
    public class Currency
    {
        private const string VALUE_REGEX = @"^[0-9-]*$";
        private const string SYMBOL_REGEX = @"^[0-9-]*$";

        public decimal Value { get; set; }
        public string Symbol { get; set; }

        public static bool TryParse(string currencyToParse, out Currency currency)
        {
            bool result = false;
            var resultCurrency = new Currency();

            Match match = Regex.Match(currencyToParse, VALUE_REGEX);
            resultCurrency.Value = decimal.Parse(match.Value);

            match = Regex.Match(currencyToParse, SYMBOL_REGEX);
            resultCurrency.Symbol = match.Value;
            currency = resultCurrency;

            return result;
        }
    }


}
