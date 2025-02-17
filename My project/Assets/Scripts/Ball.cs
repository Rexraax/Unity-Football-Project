using UnityEngine;

public class Ball : MonoBehaviour
{
    // public GameObject ballParticleEffect;
    private Rigidbody Rb;
    public float speed = 5f; // Speed of movement

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if there is a touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began) // Detect touch start
            {
                MoveForward();
                // Instantiate(ballParticleEffect, transform.position,Quaternion.identity);
            }
        }
    }

    void MoveForward()
    {
        // Move the ball forward
       Rb.linearVelocity = transform.forward * speed;
    }
}
