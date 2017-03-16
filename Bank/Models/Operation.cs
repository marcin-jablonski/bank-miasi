using System;
using Bank.Enums;

namespace Bank.Models
{
    public class Operation
    {
        public OperationType Type { get; set; }
        public DateTime Date { get; set; }
        public  string Description { get; set; }
    }
}