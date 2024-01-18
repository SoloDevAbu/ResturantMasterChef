using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlateKitchenObject : KitchenObject
{

    public event EventHandler<OnPizzaIngredientAddedEventArgs> OnPizzaIngredientAdded;
    public class OnPizzaIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validPizzaKitchenObjectSoList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }


    public bool TryAddPizzaIngredient(KitchenObjectSO kitchenObjectSO) {
        if(!validPizzaKitchenObjectSoList.Contains(kitchenObjectSO)) {
            //Not a valid ingredient
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO)) {
            //Already contains this type
            return false;
        } else{
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnPizzaIngredientAdded?.Invoke(this, new OnPizzaIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }
}
