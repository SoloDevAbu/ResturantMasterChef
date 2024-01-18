using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Player player;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_INTERACTING = "IsInteracting";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // private void Start() {
    //     player.OnPLayerGrabbedSomething += Player_OnPLayerGrabbedSomething;
    // }

    private void Update() {
        animator.SetBool(IS_RUNNING, player.IsRunning());
        // animator.SetBool(IS_INTERACTING, player.PlayerIsCarryingSomething());
    }

    // private void Player_OnPLayerGrabbedSomething(object sender, System.EventArgs e) {
    //     animator.SetBool
    // }
}
