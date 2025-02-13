using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AttachCommand : ICommand
{
    RaycastHit _hit;
    characterMovement _player;

    public AttachCommand(characterMovement player, RaycastHit hit)
    {
        _player = player;
        _hit = hit;
    }
    public void Execute()
    {
        var blob = _hit.collider.transform.parent;

            //Debug.Log("Connect Blob");
            blob.SetParent(_player.transform); //only do this is this has not been done before
            blob.tag = "BlobConnected";
    }
    public void Undo()
    {
            var blob = _hit.collider.transform.parent;
            //Debug.Log("Blob Disconnect");
            blob.SetParent(null);
            blob.tag = "BlobDisconnected";
    }
}
