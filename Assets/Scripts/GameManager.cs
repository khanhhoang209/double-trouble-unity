using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;


    [SerializeField] private TMP_Text coinText;

    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;

    //Level Complete

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;





    private int totalCoins = 0;




    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;

        FindTotalPickups();
    }

    public void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }
    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString() + "/26";

    }

    public void Death()
    {
        if (!isGameOver)
        {
            // Disable Mobile Controls
            UIManager.instance.DisableMobileControls();
            // Initiate screen fade
            UIManager.instance.fadeToBlack = true;

            // Disable the player object
            playerController.gameObject.SetActive(false);

            // Start death coroutine to wait and then respawn the player
            StartCoroutine(DeathCoroutine());

            isGameOver = true;

            // Log death message
            Debug.Log("Died");
        }
    }

    public void FindTotalPickups()
    {

        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }

        }



    }
    public void LevelComplete()
    {
        if (coinCount < 26)
        {
            levelCompletePanel.SetActive(true);
            leveCompletePanelTitle.text = "NEED MORE COINS!";
            StartCoroutine(HideLevelCompletePanel());
            return;
        }

        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "LEVEL COMPLETE";

        // StartCoroutine(LoadNextLevel());

    }

    public bool CheckLevelComplete()
    {
        if (coinCount < 26)
        {
            return false;
        }
        return true;
    }
    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        playerController.transform.position = new Vector3(Random.Range(-25, -22), 8f, 0);

        yield return new WaitForSeconds(1f);

        if (isGameOver)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }

    private IEnumerator HideLevelCompletePanel()
    {
        yield return new WaitForSeconds(2f);
        levelCompletePanel.SetActive(false);
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel(3);
    }

}
