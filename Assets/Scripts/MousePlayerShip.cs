using UnityEngine;

public class MousePlayerShip : PlayerShip
{
    protected override void HandleTargetRotation()
    {
        TargetRotation = Input.mousePosition;
        TargetRotation = Camera.main.ScreenToWorldPoint(TargetRotation);
    }
}