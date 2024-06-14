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

    //private PlayerAttack playerAttack;
    private Collider hitbox;
    private Animator animator;
    private int difficulty;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        //playerAttack = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").transform.Find("Root").transform.Find("Hips").transform.Find("Spine_01").transform.Find("Spine_02").transform.Find("Spine_03").transform.Find("Clavicle_R").transform.Find("Shoulder_R").transform.Find("Elbow_R").transform.Find("Hand_R").transform.Find("SM_Prop_SwordOrnate_01").transform.Find("weaponHitBox").gameObject.GetComponent<PlayerAttack>();
        hitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    public override void entityTakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    protected override void entityDie()
    {
        throw new System.NotImplementedException();
    }

    protected override void entityAttack()
    {
        throw new System.NotImplementedException();
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

    /*
    protected override void entityAttack()
    {
        playerAttack.playerAttack();
    }

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
