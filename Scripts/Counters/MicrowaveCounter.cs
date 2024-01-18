using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveCounter : BaseCounter
{

    public event EventHandler<OnStateChnagedEventArgs> OnStateChnaged;
    public class OnStateChnagedEventArgs : EventArgs {
        public State state;
    }

    public enum State{
        Idle,
        Frying,
        Fried,
    }
    
    [SerializeField] private FryingRecipeSO[] microwaveRecipeSOArray;

    private State state;
    private float waveTimer;
    private FryingRecipeSO microwaveRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {

        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                if(HasKitchenObejct()) {
                    waveTimer += Time.deltaTime;
                    
                    if(waveTimer > microwaveRecipeSO.fryingTimerMax) {
                        //Waved
                        
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(microwaveRecipeSO.output, this);

                        state = State.Fried;

                        OnStateChnaged?.Invoke(this, new OnStateChnagedEventArgs {
                            state = state
                        });
                    }
                }
                break;

            case State.Fried:
                break;

            
            
        }
    }

    public override void Interact(Player player) {
        if(!HasKitchenObejct()) {
            //There is no Object

            if(player.HasKitchenObejct()) {
                //Player has an object
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    //Player has something that can be microwaved
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    microwaveRecipeSO = GetMicrowaveRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    waveTimer = 0f;

                    OnStateChnaged?.Invoke(this, new OnStateChnagedEventArgs {
                        state = state
                    });
                }
            } else {
                // Player is not carrying anything
            }
        } else {
            //There is an Object
            if(player.HasKitchenObejct()) {
                //Player is carrying something
            } else {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnStateChnaged?.Invoke(this, new OnStateChnagedEventArgs {
                    state = state
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO microwaveRecipeSO = GetMicrowaveRecipeSOWithInput(inputKitchenObjectSO);
            return microwaveRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO microwaveRecipeSO = GetMicrowaveRecipeSOWithInput(inputKitchenObjectSO);
        if(microwaveRecipeSO != null) {
            return microwaveRecipeSO.output;
        } else {
            return null;
        }
    }

    private FryingRecipeSO GetMicrowaveRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO microwaveRecipeSO in microwaveRecipeSOArray) {
            if(microwaveRecipeSO.input == inputKitchenObjectSO) {
                return microwaveRecipeSO;
            }
        }
        return null;
    }
}
