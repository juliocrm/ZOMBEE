using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHurtable
{
    float Hurt(float damage, Vector3 from);
}
