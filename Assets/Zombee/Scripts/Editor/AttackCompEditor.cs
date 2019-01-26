using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AttackComp))]
public class AttackCompEditor : Editor
{
    private void OnSceneGUI()
    {
        var attackComp = target as AttackComp;

        var sphereRadious = attackComp._sphereRadious;
        var position = attackComp._overlapSphereTransform;

        Handles.DrawSolidArc(position.position, position.up,
            new Vector3(360, 360, 360), 360, sphereRadious);
    }
}
