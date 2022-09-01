using System;
using System.Collections;
using Buildings.Bank;
using Stackable;
using TMPro;
using UnityEngine;

namespace Buildings
{
    public class StoragePlace : PurchasableBuilding
    {
        [SerializeField] private StackableSystem storageStackableSystem;
        private Coroutine _storageCor;
        private Coroutine _moneyCor;
        protected override void Start()
        {
            base.Start();
            UpdateText();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!ValidateSystem(other, out var system)) return;
            if(!ValidatePurchaseStatus(system)) return;

            if (!isBuildingPurchased)
            {
                _moneyCor = StartCoroutine(MoneyCor(system));
                return;
            }

            var ownSystemType = storageStackableSystem.GetCurrentStackType();
            var systemStackType = system.GetCurrentStackType();

            if (systemStackType == StackableType.None &&
                ownSystemType == StackableType.None) return;
            
            if (ownSystemType != StackableType.None &&
                (ownSystemType != systemStackType && systemStackType != StackableType.None)) return;

            _storageCor = StartCoroutine(StorageCor(system));
        }

        private IEnumerator StorageCor(StackableSystem system)
        {
            if (system.GetCurrentStackType() != StackableType.None)
            {
                var stackableItem = system.Pop();
                do
                {
                    storageStackableSystem.Push(stackableItem.StackableData.stackableType);
                    stackableItem = system.Pop();
                    yield return wait;
                } while (stackableItem != null);
            }
            else
            {
                var stackableItem = storageStackableSystem.Pop();
                do
                {
                    system.Push(stackableItem.StackableData.stackableType);
                    stackableItem = storageStackableSystem.Pop();
                    yield return wait;
                } while ( stackableItem != null);
            }

        }

        protected override bool ValidatePurchaseStatus(StackableSystem system)
        {
            var systemStackType = system.GetCurrentStackType();
            if (isBuildingPurchased) return true;
            if (!isBuildingPurchased && systemStackType != StackableType.Money) return false;
            return true;
        }
    }
}