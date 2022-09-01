using System;
using System.Collections;
using Stackable;
using UnityEngine;

namespace Buildings.Bank
{
    public class Bank : BuildingBase
    {
        [SerializeField] private float waitTime = .1f;
        
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
            var stackable = system.Pop();
            var stackableData = system.GetCurrentStackableData();
            var itemCounter = 0;
            
            do
            {
                var price = stackable.StackableData.price;
                stackable = system.Pop();
                itemCounter++;
                yield return wait;
            } while (stackable != null);

            for (int i = 0; i < itemCounter * stackableData.price; i++)
            {
                system.Push(StackableType.Money);
                yield return wait;
            }
        }

    }
}