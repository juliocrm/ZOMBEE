using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follower : MonoBehaviour
{
    Transform followerTransform;
    public Transform targetTransform;

    public float xOffset;
    public float yOffset;
    public float zOffset;

    private void Awake()
    {
        followerTransform = transform;
    }

    private void LateUpdate()
    {
        if (targetTransform)
            FollowTarget(targetTransform.position);
    }

    private void FollowTarget(Vector3 targetPos)
    {
        Vector3 newVec = followerTransform.position;
        newVec.z = targetPos.z + zOffset;
        newVec.x = targetPos.x + xOffset;
        newVec.y = targetPos.y + yOffset;
        followerTransform.position = newVec;
    }
}