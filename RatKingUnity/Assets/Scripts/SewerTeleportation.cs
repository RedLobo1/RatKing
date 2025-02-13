using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SewerTeleportation : MonoBehaviour
{
    [SerializeField]
    private GameObject _connectedSewer;

    [SerializeField]
    private byte _SewerSize;
    private void Start()
    {

    }
    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, Vector3.up, Color.red, 3);
        var position = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, this.transform.position.z);
        if (Physics.Raycast(position, Vector3.up, out hit, 1))
        {
            //if(hit.collider.CompareTag("BlobConnected") ||) // move specific things

            if (hit.collider != null)
            {
                if (hit.collider.transform.parent == null) //player has no parent to for now this is the player
                {
                    Teleport(hit.collider.transform, Vector3.zero);
                    DetachAllChildren(hit.collider.transform);
                }
                else //this is a blob
                {
                    var blob = hit.collider.transform.parent;
                    //if (blob.transform.childCount == 1) //single blob always fir in sewer
                    //{
                    //    Teleport(hit.collider.transform.parent);
                    //    DisconnectBlob(blob);
                    //}
                    //else //can teleport only if size fits in sewer
                    //{
                    var mask = false;
                    if (this.transform.rotation.eulerAngles.y % 180 == 0)//sewer is rotated 180 degrees
                        mask = GetColliderSize(blob).x <= _SewerSize; //check x size
                    else if (this.transform.rotation.eulerAngles.y % 180 == 90) //sewer is rotated 90 degrees
                        mask = GetColliderSize(blob).z <= _SewerSize; //check z size


                    if (mask)
                    {
                        Teleport(hit.collider.transform.parent, hit.collider.transform.localPosition);
                        DisconnectBlob(blob);
                    }
                    //}
                }


            }
        }
    }

    private void DetachAllChildren(Transform playerTransform)
    {
        foreach (Transform child in playerTransform)
        {
            child.SetParent(null);
            DisconnectBlob(child);
        }
    }

    private Vector3Int GetColliderSize(Transform blob)
    {
        Bounds totalBounds = new Bounds(blob.GetChild(0).position, Vector3.zero);
        foreach (Transform child in blob)
        {
            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null)
            {
                totalBounds.Encapsulate(childCollider.bounds);
            }
        }
        return Vector3Int.RoundToInt(totalBounds.size);
        // Output the total size of the parent (based on the children's colliders)
    }

    private void DisconnectBlob(Transform blob)
    {
        if (blob.tag == "BlobConnected") //Removing parency if a connected block teleported
        {
            blob.transform.parent = null;
            blob.tag = "BlobDisconnected";
        }
    }

    private void Teleport(Transform blobTransform, Vector3 offset)
    {
        Debug.Log("Teleporting");

        //if (this.transform.rotation.y == 180)
        var pos = new Vector3(_connectedSewer.transform.position.x - _connectedSewer.transform.forward.x,
                                             blobTransform.position.y,
                                             _connectedSewer.transform.position.z - _connectedSewer.transform.forward.z);

        RaycastHit hit;
        //Debug.DrawRay(new Vector3(0, -1f, 0), Vector3.up, Color.red, 10);
        if (!Physics.Raycast(new Vector3(pos.x, pos.y - 1f, pos.z), Vector3.up, out hit, 2)) //only move when no object in the way
        {
            if (hit.collider == null)
            {
                if (blobTransform.rotation.eulerAngles.y == 180)
                {
                    offset *= -1;
                }

                blobTransform.position = new Vector3(pos.x + offset.x,
                                                 blobTransform.position.y,
                                                 pos.z + offset.z);

                float RotationAngle = Vector3.Angle(this.transform.forward, _connectedSewer.transform.forward);
                blobTransform.rotation = Quaternion.Euler(0, Mathf.RoundToInt(blobTransform.rotation.eulerAngles.y + RotationAngle), 0); //Rotating the object
            }

        }


    }
}
