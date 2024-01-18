// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class PlayerCoinsUI : MonoBehaviour
// {
//     [SerializeField] private TextMeshProUGUI totalCoins;

//     private int coins;
//     [SerializeField] private DeliveryManager deliveryManager;


//     private void Start() {
//         coins = 0;
//         deliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
//     }

//     private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e) {
//         coins += 100;
//         totalCoins.text = coins.ToString();
//     }

// }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class PlayerCoinsUI : MonoBehaviour
// {
//     [SerializeField] private TextMeshProUGUI totalCoins;
//     private int coins;
//     [SerializeField] private DeliveryManager deliveryManager;

//     private const string CoinsKey = "PlayerCoins";

//     private void Start()
//     {
//         coins = PlayerPrefs.GetInt(CoinsKey, 0);
//         UpdateCoinText();

//         deliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
//     }

//     private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
//     {
//         coins += 100;
//         UpdateCoinText();
//         SaveCoins();
//     }

//     private void UpdateCoinText()
//     {
//         totalCoins.text = coins.ToString();
//     }

//     private void SaveCoins()
//     {
//         PlayerPrefs.SetInt(CoinsKey, coins);
//         PlayerPrefs.Save();
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerCoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalCoins;
    private int coins;
    [SerializeField] private DeliveryManager deliveryManager;

    private const string CoinsKey = "PlayerCoins";

    private void Start()
    {
        coins = PlayerPrefs.GetInt(CoinsKey, 0);
        UpdateCoinText();

        deliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        StartCoroutine(IncreaseCoinsWithAnimation(coins, coins + 100, 1f)); // Adjust duration as needed
    }

    private void UpdateCoinText()
    {
        totalCoins.text = coins.ToString();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinsKey, coins);
        PlayerPrefs.Save();
    }

    private IEnumerator IncreaseCoinsWithAnimation(int startValue, int targetValue, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            coins = (int)Mathf.Lerp(startValue, targetValue, (Time.time - startTime) / duration);
            UpdateCoinText();
            yield return null;
        }

        coins = targetValue; // Ensure the final value is exactly the target value
        UpdateCoinText();
        SaveCoins();
    }
}


