using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Entitiy Stats")]
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float iFrames;
    protected float iFrameStart;


    void Awake()
    {
        health = maxHealth;
    }
    public float EntityHealth
    {
        get { return health; }
        set { health = value; }
    }

    public float EntityMaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public float EntitySpeed
    {
        get { return speed; }
        set { speed = value; }
    }
    public float EntityDamage
    {
        get { return damage; }
        set { damage = value; }
    }

    //logic for entity taking damage
    public abstract void entityTakeDamage(float damage);

    //logic for entity dying
    protected abstract void entityDie();

    //logic for entity attacking
    protected abstract void entityAttack();

}