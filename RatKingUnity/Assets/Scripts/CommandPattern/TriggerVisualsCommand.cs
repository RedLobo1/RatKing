using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
//using static UnityEditor.PlayerSettings;

public class TriggerVisualsCommand : ICommand
{
    RaycastHit _hit;
    characterMovement _player;
    characterMovement _character;
    PositionEventArgs _pos;
    void Start()
    {
        _character = GameObject.FindObjectOfType<characterMovement>();
    }

    public TriggerVisualsCommand(characterMovement player, PositionEventArgs pos)
    {
        _player = player;
        _pos = pos;
    }
    public void Execute()
    {
        _player.CallVisualEvent(_pos); //This is REALLY REALLY BAD
        //OnConnect?.Invoke(this, _pos);
    }
    public void Undo()
    {
        _player.CallUndoVisualEvent(_pos);
        CommandInvoker.Undo();
    }
}
