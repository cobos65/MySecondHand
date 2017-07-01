using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MySecondHand.Spider.Model
{
    public class Currency
    {
        private const string VALUE_REGEX = @"\d+(?:[,\.]\d{1,})?";
        private const string SYMBOL_REGEX = @"[$€£]";

        public decimal Value { get; set; }
        public string Symbol { get; set; }

        public static bool TryParse(string currencyToParse, out Currency currency)
        {
            bool result = false;
            Currency resultCurrency = null;

            try
            {
                resultCurrency = new Currency();
                Match match = Regex.Match(currencyToParse, VALUE_REGEX);
                resultCurrency.Value = decimal.Parse(match.Value.Replace(".",","));

                match = Regex.Match(currencyToParse, SYMBOL_REGEX);
                resultCurrency.Symbol = match.Value;
            }
            catch { }
            if (resultCurrency.Value != default(decimal) && !string.IsNullOrEmpty(resultCurrency.Symbol))
            {
                result = true;                
            }
            currency = resultCurrency;

            return result;
        }
    }


}
