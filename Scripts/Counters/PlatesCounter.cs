using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO platekitchenobjectSO;

    private float spawnTimer;
    private float spawnTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update() {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnTimerMax) {
            spawnTimer = 0f;

            if(platesSpawnAmount < platesSpawnAmountMax) {
                platesSpawnAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if(!player.HasKitchenObejct()) {
            //Player hand is empty
            if(platesSpawnAmount > 0) {
                platesSpawnAmount--;

                KitchenObject.SpawnKitchenObject(platekitchenobjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
