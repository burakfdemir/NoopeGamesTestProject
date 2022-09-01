using System;
using System.Collections;
using Stackable;
using TMPro;
using UnityEngine;

namespace Buildings.Bank
{
    public class Bakery : PurchasableBuilding
    {
        private Coroutine _productionCor;
        private Coroutine _moneyCor;

        protected override void Start()
        {
            base.Start();
            UpdateText();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!ValidateSystem(other, out var system)) return;
            if(!ValidatePurchaseStatus(system)) return;

            if (isBuildingPurchased) _productionCor = StartCoroutine(ProductCor());
            else _moneyCor = StartCoroutine(MoneyCor(system));
        }

        private IEnumerator ProductCor()
        {
            yield break;
        }
    }
}