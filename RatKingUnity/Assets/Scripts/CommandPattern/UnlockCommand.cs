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
    }
    public void Undo()
    {
        GameObject[] lockArray = GameObject.FindGameObjectsWithTag("Lock");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Lock").Length; i++)
        {
            lockArray[i].GetComponent<BoxCollider>().enabled = true;
            lockArray[i].GetComponentInChildren<SpriteRenderer>().enabled=true;
        }
        CommandInvoker.Undo();
    }
}
