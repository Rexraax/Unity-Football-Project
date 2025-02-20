using UnityEditor.Rendering;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Rigidbody Rb;
    public float shotPower = 10f; // Power of the shot
    public AudioClip boomSound;
    private AudioSource audioSource;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isDragging = false;

    public LineRenderer lineRenderer; // Line Renderer for dragging line
    public float lineMultiplier = 0.05f; // Adjust line length

    public GameObject gameOver;

    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        Rb = GetComponent<Rigidbody>();

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>(); // Get LineRenderer component
        }
        lineRenderer.positionCount = 2; // Line has two points (ball & finger)
        lineRenderer.enabled = false; // Hide initially

        // Set dotted line properties
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.mainTextureScale = new Vector2(1f / 0.1f, 1); // Adjust for dots
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isDragging = true;
                    lineRenderer.enabled = true; // Show line
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        UpdateDraggingLine(touch.position);
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        endTouchPosition = touch.position;
                        ShootBall();
                        isDragging = false;
                        lineRenderer.enabled = false; // Hide line after shooting
                    }
                    break;
            }
        }
    }

    void UpdateDraggingLine(Vector2 currentTouch)
    {
        Vector3 ballPosition = transform.position;
        Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(currentTouch.x, currentTouch.y, Camera.main.nearClipPlane + 1f)); // Convert touch position to world space

        lineRenderer.SetPosition(0, ballPosition); // Start of line (ball)
        lineRenderer.SetPosition(1, touchWorldPos); // End of line (finger position)

        // Adjust texture scale for dot spacing
        float lineLength = Vector3.Distance(ballPosition, touchWorldPos);
        lineRenderer.material.mainTextureScale = new Vector2(lineLength / 0.1f, 1);
    }

    void ShootBall()
    {
        Vector2 dragVector = startTouchPosition - endTouchPosition; // Get drag direction
        Vector3 shotDirection = new Vector3(dragVector.x, 0, dragVector.y).normalized;

        Rb.linearVelocity = shotDirection * shotPower; // Apply force to the ball
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(boomSound);
        }
        else if(collision.gameObject.CompareTag("Wall"))
        { 
            
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
     public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }
}
