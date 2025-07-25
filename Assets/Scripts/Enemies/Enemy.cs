using UnityEngine;

namespace FGUIStarter.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
            
                GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
                if (spawnPoint != null)
                {
                    collision.gameObject.transform.position = spawnPoint.transform.position;
                }
                else
                {
                    // Default spawn position if no spawn point is found
                    collision.gameObject.transform.position = new Vector3(-25f, 8f, 0f);
                }
            }
        }
    }
}