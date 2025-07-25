using UnityEngine;

namespace FGUIStarter.Enemies
{
    public class Saw : Enemy
    {
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private bool rotateClockwise = true;
        
        private void Update()
        {
            float rotation = rotateClockwise ? rotationSpeed : -rotationSpeed;
            transform.Rotate(0, 0, rotation * Time.deltaTime);
        }
    }
}