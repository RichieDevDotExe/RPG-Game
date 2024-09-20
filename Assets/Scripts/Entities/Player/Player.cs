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
    private float lastPlayerAttack;

    /*
    [Header("Player Items")]
    [SerializeField] private int potions;
    [SerializeField] private int maxPotions;
    [SerializeField] private int gold;
    */

    [Header("Player Audio Libary")]
    public AudioClip swordSwingSFX;
    public AudioClip takeDamageSFX;
    public AudioClip dashSFX;
    public AudioClip potionSFX;
    public AudioClip itemCollectSFX;

    private PlayerAttack playerAttack;
    private Collider hitbox;
    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerAttack = GetComponent<PlayerAttack>();
        hitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();
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

    protected override void entityAttack()
    {
        playerAttack.playerAttack();
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
