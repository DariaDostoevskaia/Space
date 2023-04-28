using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Player
{
    public class KeyBoardPlayerShip : PlayerShip
    {
        private Quaternion _rotation;
        [SerializeField] private List<KeyCode> _rotationLeftButton;
        [SerializeField] private List<KeyCode> _rotationRightButton;

        protected override void OnStart()
        {
            _rotation = transform.rotation;
            base.OnStart();
        }

        protected override void Rotation()
        {
            transform.rotation = _rotation;
        }

        protected override void HandleTargetRotation()
        {
            var euler = GetDirection(_rotationLeftButton, Vector3.back);
            euler += GetDirection(_rotationRightButton, Vector3.forward);
            _rotation *= Quaternion.Euler(euler);
        }
    }
}