using System;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public Vector3 Position { get; }
    public GameObject Entity { get; }
    public PositionEventArgs(Vector3 position, GameObject entity)
    {
        Position = position;
        Entity = entity;
    }
}