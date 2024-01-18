using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {
        if(!HasKitchenObejct()) {
            //There is no Object

            if(player.HasKitchenObejct()) {
                //Player has an object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Player is not carrying anything
            }
        } else {
            //There is an Object
            if(player.HasKitchenObejct()) {
                //Player is carrying something

                // FOR BURGER
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player is holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else{
                    //Player is not holdin a plate but something other
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }

                // FOR PIZZA

                if(player.GetKitchenObject().TryGetPizzaPlate(out PizzaPlateKitchenObject pizzaPlateKitchenObject)) {
                    //Player is holding a pizza plate
                    if(pizzaPlateKitchenObject.TryAddPizzaIngredient(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    //Player is not holdin a plate but something other
                    if(GetKitchenObject().TryGetPizzaPlate(out pizzaPlateKitchenObject)) {
                        //Counter is holding a Pizzaplate
                        if(pizzaPlateKitchenObject.TryAddPizzaIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }


            } else {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
