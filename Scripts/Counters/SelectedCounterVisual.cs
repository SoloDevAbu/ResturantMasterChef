using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject visualGameObjectFirst;
    [SerializeField] private GameObject visualGameObjectSecond;

   private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
   }

   private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e){

        if(e.selectedCounter == baseCounter) {
            Show();
        } else {
            Hide();
        }
   }

   private void Show() {
        visualGameObjectFirst.SetActive(true);
        visualGameObjectSecond.SetActive(true);

   }
   private void Hide() {
        visualGameObjectFirst.SetActive(false);
        visualGameObjectSecond.SetActive(false);

   }
}
