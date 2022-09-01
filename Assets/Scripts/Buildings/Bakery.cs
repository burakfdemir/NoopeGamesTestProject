using System;
using System.Collections;
using Buildings.Bank;
using Stackable;
using UnityEngine;

namespace Buildings
{
    public class Bakery : PurchasableBuilding
    {
        [SerializeField] private StoragePlace storagePlace;
        private Coroutine _productionCor;
        private Coroutine _moneyCor;

        protected override void Start()
        {
            base.Start();
            UpdateText();
            OnBuildingPurchased += BuildingPurchasedHandler;
            StartCoroutine(HideStorageText());
        }

        private void OnDestroy()
        {
            OnBuildingPurchased -= BuildingPurchasedHandler;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!ValidateSystem(other, out var system)) return;
            if (!ValidatePurchaseStatus(system)) return;

            if (!isBuildingPurchased) _moneyCor = StartCoroutine(MoneyCor(system));
        }

        private void BuildingPurchasedHandler()
        {
            storagePlace.SetPurchased();
            _productionCor = StartCoroutine(ProductCor());
        }

        private IEnumerator ProductCor()
        {
            var productCreationWait = new WaitForSeconds(data.productCreationTime);
            yield return productCreationWait;

            while (true)
            {
                storagePlace.Push(StackableType.Money);
                yield return productCreationWait;
            }
        }

        private IEnumerator HideStorageText()
        {
            yield return wait;
            storagePlace.HideText();
        }
    }
}