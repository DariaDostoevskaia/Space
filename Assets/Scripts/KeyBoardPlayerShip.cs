using System.Collections.Generic;
using UnityEngine;

public class KeyBoardPlayerShip : PlayerShip
{
    [SerializeField] private List<KeyCode> _rotationLeftButton;
    [SerializeField] private List<KeyCode> _rotationRightButton;

    protected override void HandleTargetRotation()
    {
        TargetRotation += GetDirection(_rotationLeftButton, new Vector3(-1, -1, 0));
        TargetRotation += GetDirection(_rotationRightButton, new Vector3(1, 1, 0));

        TargetRotation.Normalize();
    }
}