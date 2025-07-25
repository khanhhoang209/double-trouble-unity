using UnityEngine;

namespace FGUIStarter.Enemies
{
    public class SpikeUp : Enemy
    {
        [SerializeField] private float moveDistance = 2f;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float waitTime = 2f;

        private Vector2 startPosition;
        private bool movingUp = false;
        private float timer = 0f;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                timer = 0f;
                movingUp = !movingUp;
            }

            Vector2 targetPosition = startPosition;
            if (movingUp)
            {
                targetPosition.y += moveDistance;
            }

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }
}