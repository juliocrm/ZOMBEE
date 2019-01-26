using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}

public class GameManager : MonoBehaviour
{
    public WeaponDef[] weaponDefs;
}
