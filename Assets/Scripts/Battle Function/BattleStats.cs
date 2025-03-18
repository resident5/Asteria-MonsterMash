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

    [Header("Base Stats")]
    [SerializeField] private float baseMaxHealth;
    [SerializeField] private int baseStrength;
    [SerializeField] private int baseMagic;
    [SerializeField] private int baseSpeed;

    [Header("Current Stats")]
    [SerializeField] private float health;
    [SerializeField] float maxHealth;
    [SerializeField] private int strength;
    [SerializeField] private int magic;
    [SerializeField] private int speed;
    [SerializeField] private int luck;

    [SerializeField] private float mana;
    [SerializeField] private float lust;

    //Properties
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Clamp(value, 0, MAX_HP);
        //set => maxHealth = Mathf.Clamp(Mathf.RoundToInt(baseMaxHealth * Mathf.Pow(1.2f, level - 1)), 0, MAX_HP);

    }
    public int Strength
    {
        get => strength;
        set => strength = Mathf.Clamp(value, 0, MAX_STR);

    }
    public int Magic
    {
        get => magic;
        set => magic = Mathf.Clamp(value, 0, MAX_MAG);
    }
    public int Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, MAX_SPD);
    }
    public float Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, MaxHealth);
    }
    public float Mana
    {
        get => mana;

        set => mana = Mathf.Clamp(value, 0, MAX_MANA);

    }
    public float Lust
    {
        get => lust;

        set => lust = Mathf.Clamp(value, 0, MAX_LUST);

    }

    public int level;
    
    public void InitStats()
    {
        MaxHealth = Mathf.Clamp(Mathf.RoundToInt(baseMaxHealth * Mathf.Pow(1.2f, level - 1)), 0, MAX_HP);

        Strength = Mathf.Clamp(Mathf.RoundToInt(baseStrength * Mathf.Pow(1.2f, level - 1)), 0, MAX_STR);
        Magic = Mathf.Clamp(Mathf.RoundToInt(baseMagic * Mathf.Pow(1.2f, level - 1)), 0, MAX_MAG);
        Speed = Mathf.Clamp(Mathf.RoundToInt(baseSpeed * Mathf.Pow(1.2f, level - 1)), 0, MAX_SPD);

        Health = MaxHealth;

    }

    public void LevelUp()
    {
        level++;

        InitStats();

    }
}
