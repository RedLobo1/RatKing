using System;
using System.Collections.Generic;
using UnityEngine;

public class GrandChildrenEventArgs : EventArgs
{
    public List<GameObject> GrandChildren { get; }

    public GrandChildrenEventArgs(List<GameObject> Children)
    {
    }
}