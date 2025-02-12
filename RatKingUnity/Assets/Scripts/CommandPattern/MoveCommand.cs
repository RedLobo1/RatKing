using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCommand : ICommand
{
    Vector3 _direction;
    characterMovement _player;
    public MoveCommand(Vector3 direction, characterMovement player)
    {
        _direction = direction;
        _player = player;
    }
    public void Execute()
    {
        Debug.Log("Move");
        if (_player.CanIMove(_direction))
        {
            _player.transform.position = _player.transform.position + _direction;
        }
        else
        {
            _direction = Vector3.zero;
        }
        
        //_player.UpdatePosition(_direction,true);
    }
    public void Undo()
    {

        Debug.Log("UnMove");
        _player.transform.position = _player.transform.position - _direction;

        //_player.UpdatePosition(-_direction, false);
    }
}
