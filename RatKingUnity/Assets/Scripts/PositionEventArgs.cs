using System;
using UnityEngine;

public class PositionEventArgs : EventArgs
{
    public Vector3 Position { get; }

    public PositionEventArgs(Vector3 position)
    {
        Position = position;
    }
}
