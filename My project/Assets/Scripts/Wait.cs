using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float waitingTime = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wait_For_Intro();
    }

    IEnumerator Wait_For_Intro()
    {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene("1");

    }

    
   
}
