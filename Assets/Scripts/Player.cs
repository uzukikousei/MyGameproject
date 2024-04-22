using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour,IKitchenObjectParent
{
    public static Player Instance { get; private set; }




    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float Speed = 7f; //����
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask CounterLayermask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    private bool isWalking;
    private BaseCounter selectedCounter; // ѡ��Ч��
    private Vector3 lastInteration; //��󽻻����ĵĽ�������
    private KitchenObject kitchenObject;



    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("error");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        //�ƶ�
        HandleMovement();  
        //����
        HandleInteration();
    }

    

    private void HandleInteration()
    {
        Vector2 InputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);

        if(moveDir !=Vector3.zero)
        {
            lastInteration = moveDir;
        }

        
        float interatDistance = 2f; //��������
        if (Physics.Raycast(transform.position, lastInteration,out RaycastHit raycastHit, interatDistance,CounterLayermask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 InputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, 0, InputVector.y);

        //�����ײ

        
        float PlayerHight = 1f; //��Ҹ߶�
        
        float moveDistance = Speed * Time.deltaTime; //�ƶ�����
        
        float PlayerSize = .7f; //�����ײ����
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerSize, moveDir, moveDistance);

        if (!canMove)
        {
            //��������ƶ�����X���ƶ�;
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerSize, moveDirx, moveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            }
            else
            {
                //��������ƶ�����Z���ƶ�;
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerSize, moveDirz, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirz;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        
        isWalking = moveDir != Vector3.zero; //�ƶ�����

        
        float rotateSpeed = 10f; // ��תʱ��
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
    public Transform GetKitchenObjectFollowTransfrom()
    {
        return KitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
