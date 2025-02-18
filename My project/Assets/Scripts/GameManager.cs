// using UnityEngine;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public GameObject gameCompletePanel;  // Assign from Inspector
//     public GameObject gameOverPanel;      // Assign from Inspector
//     private Enemy[] enemies;

//     void Start()
//     {
//         gameCompletePanel.SetActive(false);
//         gameOverPanel.SetActive(false);
//         UpdateEnemyList();
//     }

//     void Update()
//     {
//         CheckGameComplete();
//     }

//     // Check if all enemies are destroyed
//     void CheckGameComplete()
//     {
//         if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
//         {
//             ShowGameComplete();
//         }
//     }

//     public void ShowGameComplete()
//     {
//         gameCompletePanel.SetActive(true);
//         Time.timeScale = 0f; // Pause game
//     }

//     public void ShowGameOver()
//     {
//         gameOverPanel.SetActive(true);
//         Time.timeScale = 0f; // Pause game
//     }
// }
