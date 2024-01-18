using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplet;

    private void Awake() {
        iconTemplet.gameObject.SetActive(false);
    }

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {

        foreach(Transform child in transform) {
            if(child == iconTemplet) 
            continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
            Transform iconTransform = Instantiate(iconTemplet, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
