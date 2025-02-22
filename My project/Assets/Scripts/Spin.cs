using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,spinSpeed*Time.deltaTime,0);
    }
}
