using MySecondHand.Spider.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MySecondHand.Spider.Unit.Tests.Model
{    
    public class CurrencyTest
    {
        [Fact]
        public void TryParseWithValidValueShouldReturnNewCurrency()
        {
            var currencyString = "";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, 1);
            Assert.Equal(outCurrency.Symbol, "€");
        }

        [Fact]
        public void TryParseWithWrogValueShouldReturnNull()
        {
            var currencyString = "";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, 1);
            Assert.Equal(outCurrency.Symbol, "€");
        }
    }
}
