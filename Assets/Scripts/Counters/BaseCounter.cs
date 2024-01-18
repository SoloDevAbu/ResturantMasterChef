using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player) {
        // Debug.LogError("BaseCounter.InteractAlternate();");
    }

    
    [SerializeField] private Transform counterTopPoint;
   

    private KitchenObject kitchenObject;
    
    

    public Transform GetkitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObejct() {
        return kitchenObject != null;
    }
}
