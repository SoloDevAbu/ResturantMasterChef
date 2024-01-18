using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterVisual : MonoBehaviour
{

    private const string IS_TRASHING = "IsTrashing";
    [SerializeField] private TrashCounter trashCounter;


    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        trashCounter.OnPlayerTrashedObject += TrashCounter_OnPlayerTrashedObject;
    }

    private void TrashCounter_OnPlayerTrashedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(IS_TRASHING);
    }
}
