using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    public float TimeToActive;
    public float TimeActive;
    public int Damage;
}

public class GameManager : MonoBehaviour
{
    private MusicManager _musicManager;
    public static GameManager Get { get; private set; }

    void Awake()
    {
        Get = this;
        _musicManager = GetComponentInChildren<MusicManager>();
        Assert.IsNotNull(_musicManager, "Falta el MusicManager");
    }
    void Start()
    {
        _musicManager.Init();

    }

    public WeaponDef[] weaponDefs =
    {
        new WeaponDef //Light
            { damage = 25, durability = 4},
        new WeaponDef //Medium
            { damage = 25, durability = 7},
        new WeaponDef //Heavy
            { damage = 330, durability = 1},
    };
}

