using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardOnDelivery : MonoBehaviour
{
    [SerializeField] private GameObject pileOfCoinParent;
    [SerializeField] private Vector3[] initialPositions;
    [SerializeField] private Quaternion[] initialRotations;
    [SerializeField] private int noOfCoins;


    private void Start() {

        initialPositions = new Vector3[noOfCoins];
        initialRotations = new Quaternion[noOfCoins];


        for(int i = 0; i < pileOfCoinParent.transform.childCount; i++) {
            initialPositions[i] = pileOfCoinParent.transform.GetChild(i).position;
            initialRotations[i] = pileOfCoinParent.transform.GetChild(i).rotation;
        }

        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void Reset() {
        for(int i = 0; i < pileOfCoinParent.transform.childCount; i++) {
            pileOfCoinParent.transform.GetChild(i).position = initialPositions[i];
            pileOfCoinParent.transform.GetChild(i).rotation = initialRotations[i];
        }
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
        RewarPileOfCoins(noOfCoins);
    }

    public void RewarPileOfCoins(int noCoin) {
        Reset();

        var delay = 0f;

        pileOfCoinParent.SetActive(true);

        for(int i = 0; i < pileOfCoinParent.transform.childCount; i++) {
            pileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.3f).SetEase(Ease.OutBack);
            pileOfCoinParent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(98f, 345f), 1f).SetDelay(delay + 0.5f).SetEase(Ease.OutBack);

            pileOfCoinParent.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f);

            pileOfCoinParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1f).SetEase(Ease.OutBack);

            delay += 0.2f;
        }
    }
}
