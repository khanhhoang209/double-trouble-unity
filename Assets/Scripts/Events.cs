using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviourPunCallbacks
{
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void Level()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void ContinueGame()
    {
        PhotonNetwork.LoadLevel(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
