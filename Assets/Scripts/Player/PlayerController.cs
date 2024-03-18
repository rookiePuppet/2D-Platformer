using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    #region Components

    public PlayerStateMachine StateMachine { get; private set; }
    public Core Core { get; private set; }
    [SerializeField] private PlayerStatesConfigSO playerStatesConfigSo;
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private Transform dashDirectionIndicator;

    public PlayerStatesConfigSO StatesConfigSo => playerStatesConfigSo;
    public PlayerStatsSO PlayerStats => stats;
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public WeaponsHolder WeaponsHolder { get; private set; }
    public Transform DashDirectionIndicator => dashDirectionIndicator;

    private BoxCollider2D _collider;

    #endregion

    public float Health { get; set; } = 100f;
    public event Action<DamageInfo> Damaged;

    #region Unity Callback Functions

    private void OnEnable()
    {
        Damaged += OnDamaged;
        WeaponsHolder.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        Damaged += OnDamaged;
        WeaponsHolder.WeaponChanged -= OnWeaponChanged;
    }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Animator = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        WeaponsHolder = GetComponentInChildren<WeaponsHolder>();
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

    private void OnDamaged(DamageInfo info)
    {
        // var direction = info.hitSourcePosition.x > transform.position.x ? -1 : 1;
        // Core.Movement.SetVelocity(info.knockBackVelocity.x * direction, info.knockBackVelocity.y);
    }

    public void TakeDamage(DamageInfo info)
    {
        stats.TakeDamage(info);
        Damaged?.Invoke(info);
    }

    private void OnWeaponChanged()
    {
        var primaryWeapon = WeaponsHolder.Weapons[(int)CombatInputs.Primary];
        var secondaryWeapon = WeaponsHolder.Weapons[(int)CombatInputs.Secondary];

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