using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    #region Components

    public PlayerStateMachine StateMachine { get; private set; }
    public Core Core { get; private set; }
    [SerializeField] private PlayerStatesConfigSO playerStatesConfigSo;
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Transform dashDirectionIndicator;
    [SerializeField] private Transform weaponHolderTransform;

    public PlayerStatesConfigSO StatesConfigSo => playerStatesConfigSo;
    public PlayerStatsSO PlayerStats => stats;
    public InventoryManager InventoryManager => inventoryManager;
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Transform DashDirectionIndicator => dashDirectionIndicator;
    public Transform WeaponHolderTransform => weaponHolderTransform;

    private BoxCollider2D _collider;

    #endregion

    public float Health
    {
        get => stats.Health;
        set => stats.Health = value;
    }

    public event Action<DamageInfo> Damaged;
    public event Action Dead;

    #region Unity Callback Functions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Animator = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        Dead += OnDead;
        inventoryManager.WeaponChanged += OnWeaponChanged;
        Debug.Log("Player OnEnable");
    }

    private void OnDisable()
    {
        Dead -= OnDead;
        inventoryManager.WeaponChanged -= OnWeaponChanged;
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine(this);
        inventoryManager.Player = this;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        PlayerStats.RecoverDashEnergy();
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

    public void SetCrouchCollider()
    {
        var temp = _collider.size;
        temp.y = playerStatesConfigSo.crouchColliderHeight;
        _collider.size = temp;

        temp = _collider.offset;
        temp.y = playerStatesConfigSo.normalColliderYOffset -
                 (playerStatesConfigSo.normalColliderHeight - playerStatesConfigSo.crouchColliderHeight) / 2;
        _collider.offset = temp;
    }

    public void SetNormalCollider()
    {
        var temp = _collider.size;
        temp.y = playerStatesConfigSo.normalColliderHeight;
        _collider.size = temp;

        temp = _collider.offset;
        temp.y = playerStatesConfigSo.normalColliderYOffset;
        _collider.offset = temp;
    }

    #endregion

    private async void OnDead()
    {
        await sceneLoader.LoadSceneAsync("StartScene");
    }

    public void TakeDamage(DamageInfo info)
    {
        stats.TakeDamage(info);
        Damaged?.Invoke(info);
        
        // var direction = info.hitSourcePosition.x > transform.position.x ? -1 : 1;
        // Core.Movement.SetVelocity(info.knockBackVelocity.x * direction, info.knockBackVelocity.y);

        if (stats.Health <= 0) Dead?.Invoke();
    }

    private void OnWeaponChanged(Weapon primaryWeapon, Weapon secondaryWeapon)
    {
        if (primaryWeapon != null)
        {
            var primaryAttackState = StateMachine.GetStateInstance<PlayerPrimaryAttackState>();
            primaryAttackState.SetWeapon(primaryWeapon);
        }

        if (secondaryWeapon != null)
        {
            var secondaryAttackState = StateMachine.GetStateInstance<PlayerSecondaryAttackState>();
            secondaryAttackState.SetWeapon(secondaryWeapon);
        }
    }
}