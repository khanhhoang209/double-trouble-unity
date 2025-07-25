using UnityEngine;
using Photon.Pun;

namespace FGUIStarter.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PhotonNetwork.LoadLevel(3);
            }
        }
    }
}