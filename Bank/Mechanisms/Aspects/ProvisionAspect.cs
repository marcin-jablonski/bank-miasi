using System;
using System.Collections.Generic;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Bank.Mechanisms.Aspects
{
    [PSerializable]
    public class ProvisionAspect : OnMethodBoundaryAspect
    {
        public override void OnExit(MethodExecutionArgs args)
        {
            var lsit = (List<Bank>)args.Arguments[0];
            var destination = (string) args.Arguments[1];

            foreach (var bank in lsit)
            {
                if (destination.Contains(bank.GetBankId().ToString()))
                {
                    var acc =
                        bank.GetBankProduct(int.Parse(destination.Replace(bank.GetBankId().ToString(), String.Empty)));
                    acc.Amount -= 2;
                }
            }
        }
    }
}