using System.Collections;
using Stackable;
using TMPro;
using UnityEngine;

namespace Buildings.Bank
{
    public class PurchasableBuilding : BuildingBase
    {
        [SerializeField] protected TextMeshProUGUI priceText;
        protected bool isBuildingPurchased;
        protected int currentGivenMoney;
        protected virtual void UpdateText()
        {
            priceText.text = GetInfoText();
        }

        protected virtual string GetInfoText()
        {
            return !isBuildingPurchased
                ? $"{data.buildingName}{data.buildingPrice}$/{currentGivenMoney}$"
                : $"{data.buildingName}";
        }

        protected virtual bool ValidatePurchaseStatus(StackableSystem system)
        {
            var stackType = system.GetCurrentStackType();

            if (stackType != StackableType.Money && !isBuildingPurchased) return false;
            if (isBuildingPurchased && stackType == StackableType.Money) return false;

            return true;
        }
        
        protected virtual IEnumerator MoneyCor(StackableSystem system)
        {
            var price = data.buildingPrice;
            var stackableItem = system.Pop();
            var moneyCounter = 0;
            
            do
            {
                moneyCounter++;
                currentGivenMoney += Bank.oneUnitMoneyPrice; 
                if (currentGivenMoney >= price)
                {
                    isBuildingPurchased = true;
                    UpdateText();
                    yield break;
                }
                UpdateText();
                
                stackableItem = system.Pop();
                yield return wait;
            } 
            while (stackableItem != null);
        }

        public void SetPurchased() => isBuildingPurchased = true;
    }
}