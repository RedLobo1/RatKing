using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.Experimental.GraphView.GraphView;
//using static UnityEditor.PlayerSettings;

public class TeleportationCommand : ICommand
{
    Vector3 _originalPosition;
    Transform _entity;
    Quaternion _originalRotation;
    public TeleportationCommand(Vector3 originalPosition, Transform entity, Quaternion originalRotation) 
    {
        _originalPosition = originalPosition;
        _entity = entity;
        _originalRotation = originalRotation;
    }  
    public void Execute()
    {
    }
    public void Undo()
    {
        _entity.transform.position = _originalPosition;
        _entity.transform.rotation = _originalRotation;
        CommandInvoker.Undo();
    }
}
