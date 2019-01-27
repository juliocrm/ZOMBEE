using UnityEngine;

public class EnemyDrop : Entity
{
    [SerializeField]
    private GameObject _dropWeaponPrefab;

    [SerializeField]
    private float probability = 0.5f;

    private const float spawnHeight = 1;

    public override void Die()
    {
        var value = Random.Range(0,101);
        if(value <= probability * 100)
        {
            DropWeapon();
        }
    }

    private void DropWeapon()
    {
        var spawnPos = transform.position;
        spawnPos.y += spawnHeight;

        Instantiate(_dropWeaponPrefab, spawnPos, Quaternion.identity);
    }
}
