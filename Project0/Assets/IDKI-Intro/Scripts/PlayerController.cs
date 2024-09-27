using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move the vehicle forward
        Vector3 movementDirection = new Vector3(h, 0f, v);

        transform.Translate( movementDirection * Time.deltaTime * speed);

        if(Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log("Restarting game");
            RestartGame();
        }
    }


    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
