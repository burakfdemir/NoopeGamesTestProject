using System;
using System.Collections;
using Stackable;
using UnityEngine;

namespace Buildings.Bank
{
    public class Bank : BuildingBase
    {
        private Coroutine _bankCor;

        public static int oneUnitMoneyPrice = 1000;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!ValidateSystem(other, out var system)) return;
            if(!system.IsCurrentStackPurchasable()) return;

            _bankCor = StartCoroutine(BankOperationCor(system));
        }

        private IEnumerator BankOperationCor(StackableSystem system)
        {
            var stackableDataPrice = system.GetCurrentStackableData().price;
            var stackable = system.Pop();
            var itemCounter = 0;
            
            do
            {
                var price = stackable.StackableData.price;
                stackable = system.Pop();
                itemCounter++;
                yield return wait;
            } while (stackable != null);

            for (int i = 0; i < itemCounter * stackableDataPrice; i++)
            {
                system.Push(StackableType.Money);
                yield return wait;
            }
        }

    }
}