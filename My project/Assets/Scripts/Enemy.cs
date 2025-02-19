using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject particleEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.EnemyDestroyed();
        }
    }
}
