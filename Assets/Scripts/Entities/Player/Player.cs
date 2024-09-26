using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //Make Player a singleton
    public static Player instance;

    // Start is called before the first frame update
    [Header("Player Stats")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject playerHeightTransform;
    private float playerHeight;
    private float lastPlayerAttack;

    /*
    [Header("Player Items")]
    [SerializeField] private int potions;
    [SerializeField] private int maxPotions;
    [SerializeField] private int gold;
    */
    private Vector3 lookTarget;

    [Header("Player Audio Libary")]
    public AudioClip swordSwingSFX;
    public AudioClip takeDamageSFX;
    public AudioClip dashSFX;
    public AudioClip potionSFX;
    public AudioClip itemCollectSFX;

    private PlayerAttack playerAttack;
    private Collider hitbox;
    private Animator animator;
    private MeshSockets sockets;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerAttack = GetComponent<PlayerAttack>();
        hitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();

        playerHeight = playerHeightTransform.transform.position.y;
    }
    private void Start()
    {
        
    }

    public float LastPlayerAttack
    {
        get { return lastPlayerAttack; }
        set { lastPlayerAttack = value; }
    }

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }
    public Collider Hitbox
    {
        get { return hitbox; }
        set { hitbox = value; }
    }

    public Animator PlayerAnimator
    {
        get { return animator; }
        set { animator = value; }
    }
    public PlayerAttack PlayerAttack
    {
        get { return playerAttack; }
        set { playerAttack = value; }
    }
    public MeshSockets Sockets { 
        get { return sockets; } 
        set { sockets = value; }
    }
    public Vector3 LookTarget
    {
        get { return lookTarget; }
        set { lookTarget = value; }
    }
    public float PlayerHeight
    {
        get { return playerHeight; }
        set { playerHeight = value; }
    }

    protected override void entityAttack()
    {
        playerAttack.startAttack();
    }

    //logic for entity taking damage
    public override void entityTakeDamage(float damage)
    {

    }

    //logic for entity dying
    protected override void entityDie()
    {

    }

    //logic for entity attacking

    /*
    protected override void entityDie()
    {
        Destroy(gameObject);
    }

    //unsure if this is normal
    public void playerDie()
    {
        entityDie();
    }
    

    public override void entityTakeDamage(float damage)
    {
        if (Time.time - iFrameStart >= iFrames)
        {
            health -= damage;
            SoundFXManager.instance.playSoundEffect(takeDamageSFX, transform, 1f);
            iFrameStart = Time.time;
        }
    }
    */

}
