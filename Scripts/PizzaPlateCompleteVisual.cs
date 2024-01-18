using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlateCompleteVisual : MonoBehaviour
{

    // private int pizzaIngredientTotalItemCount = 9;
    // private int pizzaIngredientItemCount;

    [Serializable]
    public struct PizzaKitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }


    [SerializeField] private PizzaPlateKitchenObject pizzaPlateKitchenObject;
    [SerializeField] private List<PizzaKitchenObjectSO_GameObject> pizzaKitchenObjectSOGameObjectList;

    private void Start() {
        pizzaPlateKitchenObject.OnPizzaIngredientAdded += PizzaPlateKitchenObject_OnPizzaIngredientAdded;

        foreach(PizzaKitchenObjectSO_GameObject pizzaKitchenObjectSOGameObject in pizzaKitchenObjectSOGameObjectList) {
            pizzaKitchenObjectSOGameObject.gameObject.SetActive(false);
        }
        // pizzaUncookedCompleteVisual.SetActive(false);
    }

    private void PizzaPlateKitchenObject_OnPizzaIngredientAdded(object sender, PizzaPlateKitchenObject.OnPizzaIngredientAddedEventArgs e) {
        foreach(PizzaKitchenObjectSO_GameObject pizzaKitchenObjectSOGameObject in pizzaKitchenObjectSOGameObjectList) {
            if(pizzaKitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO) {
                pizzaKitchenObjectSOGameObject.gameObject.SetActive(true);
                // pizzaIngredientItemCount++;
            }

            // if(pizzaIngredientItemCount == pizzaIngredientTotalItemCount) {
            //     pizzaPlateKitchenObject.DestroySelf();                

            //     pizzaIngredientItemCount = 0;
            // }
        }
    }


}
