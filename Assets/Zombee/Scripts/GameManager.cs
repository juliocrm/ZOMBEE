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
    private MusicManager _musicManager;

    void Awake()
    {
        _musicManager = GetComponentInChildren<MusicManager>();
        Assert.IsNotNull(_musicManager, "Falta el MusicManager");
    }

    void Start()
    {
        _musicManager.Init();

        StartCoroutine(_Test());
    }

    public WeaponDef[] weaponDefs;
}
