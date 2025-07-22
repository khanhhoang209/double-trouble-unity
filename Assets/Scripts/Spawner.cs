using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-25, -22), 8f, 0), Quaternion.identity);

        if (virtualCam != null && player != null)
        {
            virtualCam.Follow = player.transform;
        }
    }
}
