using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
//using static UnityEditor.PlayerSettings;

public class UnlockCommand : ICommand
{
    public void Execute()
    {
        GameObject[] lockArray = GameObject.FindGameObjectsWithTag("Lock");
        for (int i = 0; i < lockArray.Length; i++)
        {
            lockArray[i].GetComponent<BoxCollider>().enabled = false;
            lockArray[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
            Debug.Log(i);
        }

        GameObject[] hingeArray = GameObject.FindGameObjectsWithTag("Hinge");
        for (int i = 0; i < hingeArray.Length; i++)
        {
            hingeArray[i].GetComponent<SpriteRenderer>().sprite = hingeArray[i].GetComponent<Hinge>().Open;
        }
    }
    public void Undo()
    {
        GameObject[] lockArray = GameObject.FindGameObjectsWithTag("Lock");
        for (int i = 0; i < lockArray.Length; i++)
        {
            lockArray[i].GetComponent<BoxCollider>().enabled = true;
            lockArray[i].GetComponentInChildren<SpriteRenderer>().enabled=true;
        }

        GameObject[] hingeArray = GameObject.FindGameObjectsWithTag("Hinge");
        for (int i = 0; i < hingeArray.Length; i++)
        {
            hingeArray[i].GetComponent<SpriteRenderer>().sprite = hingeArray[i].GetComponent<Hinge>().Closed;
        }

        CommandInvoker.Undo();
    }
}
