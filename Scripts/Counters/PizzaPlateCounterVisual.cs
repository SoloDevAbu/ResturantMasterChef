using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PizzaPlatesCounter pizzaPlatesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform PizzaPlateVisualPrefab;

    private List<GameObject> pizzaPlateVisualGameObjectList;

    private void Awake() {
        pizzaPlateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        pizzaPlatesCounter.OnPizzaPlateSpawned += PizzaPlatesCounter_OnPlateSpawned;
        pizzaPlatesCounter.OnPizzaPlateRemoved += PizzaPlatesCounter_OnPlateRemoved;
    }

    private void PizzaPlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject pizzaPlateGameObject = pizzaPlateVisualGameObjectList[pizzaPlateVisualGameObjectList.Count - 1];
        pizzaPlateVisualGameObjectList.Remove(pizzaPlateGameObject);
        Destroy(pizzaPlateGameObject);
    }

    private void PizzaPlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform pizzaPlateVisualTransform = Instantiate(PizzaPlateVisualPrefab, counterTopPoint);

        float plateOffset = 0.1f;
        pizzaPlateVisualTransform.localPosition = new Vector3(0, plateOffset * pizzaPlateVisualGameObjectList.Count, 0);
        pizzaPlateVisualGameObjectList.Add(pizzaPlateVisualTransform.gameObject);
    }

}
