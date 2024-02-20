using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components

    public PlayerStateMachine StateMachine { get; private set; }
    public Core Core { get; private set; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform dashDirectionIndicator;

    public PlayerData Data => playerData;
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public Transform DashDirectionIndicator => dashDirectionIndicator;
    private BoxCollider2D _collider;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Animator = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    #endregion

    #region Set Functions

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    #endregion

    public void SetCrouchCollider()
    {
        var temp = _collider.size;
        temp.y = playerData.crouchColliderHeight;
        _collider.size = temp;

        temp = _collider.offset;
        temp.y = playerData.normalColliderYOffset -
                 (playerData.normalColliderHeight - playerData.crouchColliderHeight) / 2;
        _collider.offset = temp;
    }

    public void SetNormalCollider()
    {
        var temp = _collider.size;
        temp.y = playerData.normalColliderHeight;
        _collider.size = temp;

        temp = _collider.offset;
        temp.y = playerData.normalColliderYOffset;
        _collider.offset = temp;
    }
}