using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject levelComplete;
    public GameObject adPanel;
    public GameObject gameOverPanel;
    public RewardedAdManager rewardedAdManager;
    public GameObject ballPrefab;  // Ball prefab for spawning
    public Transform spawnPoint;   // Spawn position for new ball

    private bool hasUsedExtraChance = false; // Prevent multiple revives

    void Start()
    {
        levelComplete.SetActive(false);
        adPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // Save the last played level when the scene starts
        PlayerPrefs.SetInt("LastPlayedLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved Last Played Level: " + SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        levelComplete.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Level Completed!");
    }

    public void CheckGameOver()
    {
        if (!hasUsedExtraChance) 
        {
            ShowAdPanel();  // Show ad panel first
        }
        else
        {
            ShowGameOver(); // If extra chance is used, go directly to game over
        }
    }

    private void ShowAdPanel()
    {
        Time.timeScale = 0f;
        adPanel.SetActive(true);
    }

    public void WatchAd()
    {
        adPanel.SetActive(false);
        rewardedAdManager.ShowRewardedAd();
    }

    public void RevivePlayer()
    {
        hasUsedExtraChance = true;
        Time.timeScale = 1f;
        Debug.Log("Player gets one more chance!");

        RespawnPlayer();
        SpawnNewBall();
    }

    public void NoThanks()
    {
        adPanel.SetActive(false);
        ShowGameOver();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = new Vector3(0, 1, 0);
        }

        Debug.Log("Player respawned.");
    }

    private void SpawnNewBall()
    {
        if (ballPrefab != null && spawnPoint != null)
        {
            Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("New ball spawned.");
        }
        else
        {
            Debug.LogWarning("Ball prefab or spawn point is not set!");
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels available");
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void TriggerGameOver()
    {
        CheckGameOver(); // Instead of showing game over immediately, check for an ad chance
    }
    
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
