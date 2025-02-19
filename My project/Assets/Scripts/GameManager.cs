using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameCompletePanel;  // Assign from Inspector
    private int enemyCount;

    private void Start()
    {
        CountEnemy();
    }

    private void CountEnemy()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDestroyed()
    {
        enemyCount--;
        Debug.Log("Remaining Enemy: " + enemyCount);
        
        if (enemyCount <= 0)  // Ensure safety check for negative values
        {
            GameComplete();
        }
    }

    private void GameComplete()
    {
        Debug.Log("Game Complete!");
        Time.timeScale = 0f;
        gameCompletePanel.SetActive(true);
    }
}
