using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private Vector3 lastInteracDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private bool isRunning;
    private bool playerIsCarryingSomething;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is more thnan one player");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e) {

        if(selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {

        if(selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }
    
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero) {
            lastInteracDir = moveDir;
        }
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteracDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                //Has BaseCounter
                if(baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance =  moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if(!canMove) {
            //Can not MOve

            // Attempt only x direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMove) {
                // can move only on the x
                moveDir = moveDirX.normalized;
            } else {
                // can not move on the x

                // Attemt only on the z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove) {
                    moveDir = moveDirZ.normalized;
                } else {
                    // can not move any direction
                }
            }
        }
        if(canMove){
            transform.position += moveDir * moveDistance;
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        isRunning = moveDir != Vector3.zero;

        // if(moveDir != Vector3.zero) {
        //     OnPlayerRunning?.Invoke(this, EventArgs.Empty);
        // }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this. selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    private void PlayerHasSomething() {
        if(HasKitchenObejct()) {
            playerIsCarryingSomething = true;
        }
    }

    public bool PlayerIsCarryingSomething() {
        return playerIsCarryingSomething;
    }

    public bool IsRunning() {
        return isRunning;
    }

    
    public Transform GetkitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
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
