using Bank.Mechanisms.Interests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BankTest
{
    public class InterestsTest
    {
           
        [Fact]
        public void _5PercentInterestTest()
        {
            _5PercentInterest _5percent = new _5PercentInterest();
            double amount = 100;
            Assert.Equal(amount*1.05,_5percent.ChargeInterest(amount));
        }

        [Fact]
        public void NoInterestTest()
        {
            NoInterest noInterest= new NoInterest();
            double amount = 100;
            Assert.Equal(amount, noInterest.ChargeInterest(amount));
        }

        [Fact]
        public void _10PercentInterestTest1()
        {
            _10PercentInterest _10percent = new _10PercentInterest();
            double amount = 100;
            Assert.Equal(amount * 1.1, _10percent.ChargeInterest(amount));
        }

        [Fact]
        public void _10PercentInterestTest2()
        {
            _10PercentInterest _10percent = new _10PercentInterest();
            double amount = 20000;
            Assert.Equal(amount * 1.05, _10percent.ChargeInterest(amount));
        }

        [Fact]
        public void _10PercentInterestTest3()
        {
            _10PercentInterest _10percent = new _10PercentInterest();
            double amount = 10000;
            Assert.Equal(amount * 1.05, _10percent.ChargeInterest(amount));
        }


    }
}
