using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponDef
{
    public int damage;
    public int durability;
}

public enum TrapType
{
    Damage,
    Weak,
    Slow,
    Turn
}

[System.Serializable]
public struct TrapDef
{
    public TrapType TrapType;
    public int TimeToActive;
    public int TimeActive;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Get { get; private set; }
    private void Awake()
    {
        Get = this;
    }
    public WeaponDef[] weaponDefs;
}

