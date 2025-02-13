using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlobConnected" || other.tag == "Player")
        {
            if (GameObject.FindGameObjectsWithTag("BlobDisconnected").Length == 0)
            {
                Debug.Log("It's a win!");
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("It's a loose");
            }
        }
    }
}
