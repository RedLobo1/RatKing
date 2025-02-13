using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isOn = false;
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
        isOn = true;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Button").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Button")[0].GetComponent<Button>().isOn)
            {
                continue;
            }
            else
            {
                return;
            }
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Lock").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Lock")[i]);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOn = false;

    }
}
