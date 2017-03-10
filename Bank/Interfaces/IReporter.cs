using System.Linq;
using Bank.Models;

namespace Bank.Interfaces
{
    public interface IReporter
    {
        void CreateReport(Query query);
    }
}