using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

public class UnlockCommand : ICommand
{
    public void Execute()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Lock").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Lock")[i].GetComponent<BoxCollider>().enabled = false;
            GameObject.FindGameObjectsWithTag("Lock")[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
    public void Undo()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Lock").Length; i++)
        {
            GameObject.FindGameObjectsWithTag("Lock")[i].GetComponent<BoxCollider>().enabled = true;
            GameObject.FindGameObjectsWithTag("Lock")[i].GetComponentInChildren<SpriteRenderer>().enabled=true;
        }
        CommandInvoker.Undo();
    }
}
