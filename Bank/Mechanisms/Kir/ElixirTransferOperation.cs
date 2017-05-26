using System;
using System.Collections.Generic;
using Bank.Enums;
using Bank.Mechanisms.Aspects;
using Bank.Models;
using Bank.Products;

namespace Bank.Mechanisms.Kir
{
    public class ElixirTransferOperation
    {
        [ProvisionAspect]
        public void Transfer(List<Bank> knownBanks, string sourceAccountId, string destinationAccountId, double amount)
        {
            BankAccount sourceAccount = null;
            BankAccount destinationAccount = null;
            Bank sourceBank = null;
            Bank destinationBank = null;

            foreach (var bank in knownBanks)
            {
                var bankId = bank.GetBankId().ToString();

                if (sourceAccountId.Contains(bankId))
                {
                    sourceAccount =
                        (BankAccount) bank.GetBankProduct(int.Parse(sourceAccountId.Replace(bankId, string.Empty)));
                    sourceBank = bank;
                }
                if (destinationAccountId.Contains(bankId))
                {
                    destinationAccount =
                        (BankAccount)bank.GetBankProduct(int.Parse(destinationAccountId.Replace(bankId, string.Empty)));
                    destinationBank = bank;
                }
            }

            try
            {
            sourceAccount.Withdraw(amount);
            destinationAccount.Deposit(amount);
            }
            catch (Exception)
            {
                var failedElixirOperation = new Operation
                {
                    Type = OperationType.FailedInterbankTransfer,
                    Date = DateTime.Now,
                    Description = amount.ToString()
                };

                sourceBank.GetHistory().Add(failedElixirOperation);
                destinationBank.GetHistory().Add(failedElixirOperation);
                return;
            }

            var elixirOperation = new Operation
            {
                Type = OperationType.SuccessfulInterbankTransfer,
                Date = DateTime.Now,
                Description = amount.ToString()
            };

            sourceBank.GetHistory().Add(elixirOperation);
            destinationBank.GetHistory().Add(elixirOperation);
        }
    }
}