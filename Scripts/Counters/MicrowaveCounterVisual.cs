using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveCounterVisual : MonoBehaviour
{

    [SerializeField] private MicrowaveCounter microwaveCounter;
    [SerializeField] private GameObject ovenOFGameObject;
    [SerializeField] private GameObject ovenOnGameObject;


    private void Start() {
        microwaveCounter.OnStateChnaged += MicrowaveCounter_OnStateChnaged;
    }

    private void MicrowaveCounter_OnStateChnaged(object sender, MicrowaveCounter.OnStateChnagedEventArgs e) {
        bool showVisual = e.state == MicrowaveCounter.State.Frying;
        ovenOFGameObject.SetActive(!showVisual);
        ovenOnGameObject.SetActive(showVisual);
    }


    
}
