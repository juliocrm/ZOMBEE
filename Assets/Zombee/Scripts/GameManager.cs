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

public struct TrapDef
{
    public TrapType TrapType;
    public int TimeToActive;

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

