using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleStats
{
    private const int MAX_HP = 999;
    private const int MAX_STR = 100;
    private const int MAX_MAG = 100;
    private const int MAX_SPD = 100;

    private const int MAX_MANA = 100;
    private const int MAX_LUST = 100;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int strength;
    [SerializeField] private int magic;
    [SerializeField] private int speed;

    [SerializeField] private int mana;
    [SerializeField] private int lust;

    //public BattleStats (int health, int maxHealth, int strength, int magic, int speed, int mana, int lust )
    //{
    //    this.health = health;
    //    this.maxHealth = maxHealth;
    //    this.strength = strength;
    //    this.magic = magic;
    //    this.speed = speed;
    //    this.mana = mana;
    //    this.lust = lust;
    //}

    public int Health
    {
        get => health;

        set
        {
            health = Mathf.Clamp(value, 0, MAX_HP);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;

        set
        {
            maxHealth = Mathf.Clamp(value, 0, MAX_HP);
        }
    }


    public int Strength
    {
        get => strength;

        set
        {
            strength = Mathf.Clamp(value, 0, MAX_STR);
        }
    }

    public int Mana
    {
        get => mana;

        set
        {
            mana = Mathf.Clamp(value, 0, MAX_MANA);
        }
    }

    public int Lust
    {
        get => lust;

        set
        {
            lust = Mathf.Clamp(value, 0, MAX_LUST);
        }
    }

    public int Magic
    {
        get => magic;
        set
        {
            magic = Mathf.Clamp(value, 0, MAX_MAG);
        }
    }

    public int Speed
    {
        get => speed;
        set
        {
            speed = Mathf.Clamp(value, 0, MAX_SPD);
        }
    }
}
