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
        _player.UpdatePosition(_direction);
    }
    public void Undo()
    {
        _player.UpdatePosition(-_direction);
    }
}
