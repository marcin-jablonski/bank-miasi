using System;
using System.Collections.Generic;
using Bank.Enums;
using Bank.Exceptions;
using Bank.Models;
using Bank.Products;

namespace Bank.Mechanisms.Kir
{
    public class ElixirTransferOperation
    {
        public void Transfer(List<Bank> knownBanks, string sourceAccountId, string accountId, double amount)
        {
            BankAccount sourceAccount = null;
            BankAccount destinationAccount = null;
            Bank sourceBank = null;
            Bank destinationBank = null;

            foreach (var bank in knownBanks)
            {
                var bankId = bank.GetBankId().ToString();
                if (!sourceAccountId.Contains(bankId)) continue;
                Console.WriteLine("source acc id: " + sourceAccountId);
                sourceAccount =
                    (BankAccount) bank.GetBankProduct(int.Parse(sourceAccountId.Replace(bankId, string.Empty)));
                sourceBank = bank;
                break;
            }

            if (sourceAccount == null)
                throw new NoSuchBankAccountException();

            foreach (var bank in knownBanks)
            {
                var bankId = bank.GetBankId().ToString();
                if (!accountId.Contains(bankId)) continue;
                Console.WriteLine("destination acc id: " + accountId);

                destinationAccount =
                    (BankAccount) bank.GetBankProduct(int.Parse(accountId.Replace(bankId, string.Empty)));
                destinationBank = bank;
                break;
            }

            if (destinationAccount == null)
            {
                var failedElixirOperation = new Operation
                {
                    Type = OperationType.FailedInterbankTransfer,
                    Date = DateTime.Now,
                    Description = amount.ToString()
                };

                sourceBank.GetHistory().Add(failedElixirOperation);
                throw new NoSuchBankAccountException();
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