using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlatesCounter : BaseCounter
{

    public event EventHandler OnPizzaPlateSpawned;
    public event EventHandler OnPizzaPlateRemoved;

    [SerializeField] private KitchenObjectSO pizzaPlatekitchenobjectSO;

    private float pizzaPlatesSpawnTimer;
    private float pizzaPlatesSpawnTimerMax = 4f;
    private int pizzaPlatesSpawnAmount;
    private int pizzaPlatesSpawnAmountMax = 3;

    private void Update() {
        pizzaPlatesSpawnTimer += Time.deltaTime;
        if(pizzaPlatesSpawnTimer > pizzaPlatesSpawnTimerMax) {
            pizzaPlatesSpawnTimer = 0f;

            if(pizzaPlatesSpawnAmount < pizzaPlatesSpawnAmountMax) {
                pizzaPlatesSpawnAmount++;

                OnPizzaPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if(!player.HasKitchenObejct()) {
            //Player hand is empty
            if(pizzaPlatesSpawnAmount > 0) {
                pizzaPlatesSpawnAmount--;

                KitchenObject.SpawnKitchenObject(pizzaPlatekitchenobjectSO, player);

                OnPizzaPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
