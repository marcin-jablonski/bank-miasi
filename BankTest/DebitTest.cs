using Bank.Exceptions;
using Bank.Mechanisms;
using Xunit;

namespace BankTest
{
    public class DebitTest
    {
        public DebitTest()
        {
            _debit = new Debit(DebitLimit);
        }

        private readonly Debit _debit;
        private const double DebitLimit = 50.00;
        private const double DebitToReduce = 20.00;
        private const double DebitToIncrease = 40.00;
        private const double DebitToIncreaseOverLimit = DebitLimit + 1;

        [Fact]
        public void ShouldCreateDebit()
        {
            Assert.NotNull(_debit);
        }

        [Fact]
        public void ShouldIncreaseDebit()
        {
            _debit.IncreaseDebit(DebitToIncrease);
            Assert.Equal(DebitToIncrease, _debit.GetUnpaidDebit());
        }

        [Fact]
        public void ShouldNotIncreaseDebit()
        {
            Assert.Throws(typeof(IllegalOperationException), () => _debit.IncreaseDebit(DebitToIncreaseOverLimit));
        }

        [Fact]
        public void ShouldNotReduceDebit()
        {
            Assert.Throws(typeof(IllegalOperationException), () => _debit.ReduceDebit(DebitToReduce));
        }

        [Fact]
        public void ShouldReduceDebit()
        {
            _debit.IncreaseDebit(DebitToIncrease);
            _debit.ReduceDebit(DebitToReduce);
            Assert.Equal(DebitToIncrease - DebitToReduce, _debit.GetUnpaidDebit());
        }
    }
}