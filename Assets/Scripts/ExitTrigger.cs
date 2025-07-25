using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    //public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("LevelExit");
        }
    }

    IEnumerator LevelExit()
    {
        //anim.SetTrigger("Exit");

        // Chỉ gọi LevelComplete để kiểm tra coin, không tự động chuyển scene
        if (GameManager.instance != null)
        {
            var win = GameManager.instance.CheckLevelComplete();
            if (win == false)
            {
                Debug.Log("Not enough coins to complete the level.");
            }
            else
            {
                yield return new WaitForSeconds(0.5f);

                UIManager.instance.fadeToBlack = true;
                Debug.Log("Enough coins to complete the level.");
                GameManager.instance.LevelComplete();
            }
        }
    }
}
