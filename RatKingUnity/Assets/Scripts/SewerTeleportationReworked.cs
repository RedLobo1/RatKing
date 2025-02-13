using System;
using System.Collections;
using UnityEngine;

public class SewerTeleportationReworked : MonoBehaviour
{

    public event EventHandler<PositionEventArgs> OnDisconnect;
    public event Action OnSewerExit;


    [SerializeField]
    private GameObject _connectedSewer;

    [SerializeField]
    private byte _SewerSize = 1;

    private Vector3 _teleportingPosition;
    private int _teleportingRotation;

    private bool _isTeleporting = false;

    private float _teleportDuration = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        _teleportingPosition = new Vector3(_connectedSewer.transform.position.x - _connectedSewer.transform.forward.x,
                                      0,
                                      _connectedSewer.transform.position.z - _connectedSewer.transform.forward.z); //Calculating teleporting position

        _teleportingRotation = (int)Vector3.Angle(this.transform.forward, _connectedSewer.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(this.transform.position, Vector3.up, Color.red, 3);
        RaycastHit hit;
        var position = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, this.transform.position.z); //ray starting point lowered to hit the object on top of me
        if (Physics.Raycast(position, Vector3.up, out hit, 1))
        {
            if (hit.collider != null && !_isTeleporting)
            {
                _isTeleporting = true;
                if (hit.collider.gameObject.tag == "Player")
                {
                    DisconnectAllChildren(hit.collider.transform); //disconnect this from parent
                    StartCoroutine(Teleport(hit.collider.transform));
                }
                //assumption it's a blob
                else
                {
                    var mask = false;
                    if (this.transform.rotation.eulerAngles.y % 180 == 0)//sewer is rotated 180 degrees
                        mask = GetColliderSize(hit.collider.transform.parent).x <= _SewerSize; //check x size
                    else if (this.transform.rotation.eulerAngles.y % 180 == 90) //sewer is rotated 90 degrees
                        mask = GetColliderSize(hit.collider.transform.parent).z <= _SewerSize; //check z size
                    if (mask)
                    {
                        DisconnectSingleBlob(hit.collider.transform.parent); //disconnect this from parent
                        if (CheckAllignmentWithSewer(hit.collider.transform))
                            StartCoroutine(Teleport(hit.collider.transform)); //this is base object. we must know the base in order to calculate the offset
                    }
                }
            }
        }

    }

    private bool CheckAllignmentWithSewer(Transform childTele)
    {
        foreach (Transform child in childTele.parent)
        {
            if (child != childTele)
            {
                RaycastHit hit;
                //var BackwardVector = new Vector3(-this.transform.forward.x, this.transform.forward.y, -this.transform.forward.z);
                // Debug.DrawRay(child.position, BackwardVector, Color.red, 10);

                if (Physics.Raycast(child.position, transform.forward, out hit, 2))
                {
                    if (hit.collider != null)
                    {
                        var tileType = hit.collider.gameObject.GetComponent<TileProperties>();
                        if (tileType != null)
                        {
                            if (tileType.TileType == ETileType.Wall)
                            {
                                Debug.Log("False");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }

    private void DisconnectAllChildren(Transform player)
    {
        foreach (Transform blob in player)
        {
            DisconnectSingleBlob(blob);
        }
    }

    private void DisconnectSingleBlob(Transform blob)
    {
        if (blob.tag == "BlobConnected") //Removing parency if a connected block teleported
        {
            blob.parent = null;
            blob.tag = "BlobDisconnected";
        }
    }

    //private void Teleport(Transform EntityTransform)
    //{

    //    Debug.Log("Teleporting" + index++);
    //    var offset = GetOffsetFromParent(EntityTransform); //is zero vector if parent origin overlaps with it
    //    EntityTransform = GetTopLevelObjectTransform(EntityTransform); //This will get the parent if the entity has one, otherwise it returns itself

    //    if (!IsTeleporterBlocked())
    //    {
    //        EntityTransform.position = _teleportingPosition + offset;
    //        EntityTransform.rotation = Quaternion.Euler(0, Mathf.RoundToInt(EntityTransform.rotation.eulerAngles.y + _teleportingRotation), 0); //Rotating the object
    //    }
    //}

    public IEnumerator Teleport(Transform EntityTransform)
    {
        var offset = GetOffsetFromParent(EntityTransform); //is zero vector if parent origin overlaps with it
        EntityTransform = GetTopLevelObjectTransform(EntityTransform); //This will get the parent if the entity has one, otherwise it returns itself

        //MakeInvisible(EntityTransform);
        //PositionEventArgs pos = new PositionEventArgs(Vector3.zero, EntityTransform.gameObject);
        //OnDisconnect?.Invoke(this, pos);

        //yield return new WaitForSeconds(_teleportDuration);
        if (!IsTeleporterBlocked())
        {
            float RotationAngle = Mathf.Round(Vector3.Angle(this.transform.forward, _connectedSewer.transform.forward));
            if (RotationAngle == 180)
                offset *= -1;
            if (RotationAngle == 90)
                if (this.transform.forward.x > _connectedSewer.transform.forward.x)
                    offset = Vector3.zero;
                else
                    offset = Quaternion.Euler(0, 90 - Mathf.RoundToInt(EntityTransform.rotation.eulerAngles.y),0) * offset;

            EntityTransform.position = _teleportingPosition + offset;
            EntityTransform.rotation = Quaternion.Euler(0, Mathf.RoundToInt(EntityTransform.rotation.eulerAngles.y + _teleportingRotation), 0); //Rotating the object
        }
        //MakeVisible(EntityTransform);
        //OnSewerExit?.Invoke();
        yield return new WaitForSeconds(0.1f);
        _isTeleporting = false;
    }

    private void MakeVisible(Transform entityTransform)
    {
        entityTransform.TryGetComponent<Renderer>(out Renderer renderer);
        renderer.enabled = true;
    }

    private void MakeInvisible(Transform entityTransform)
    {
        entityTransform.TryGetComponent<Renderer>(out Renderer renderer);
        renderer.enabled = false;
    }

    Transform GetTopLevelObjectTransform(Transform obj)
    {
        return obj.parent != null ? obj.parent : obj; //This will get the parent if the objct has one, otherwise it returns itself
    }
    Vector3 GetOffsetFromParent(Transform obj)
    {
        return obj.parent != null ? obj.localPosition : Vector3.zero; //This will get the localPosition if the objct has a parent, otherwise it returns xero vector
    }

    private bool IsTeleporterBlocked()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(_teleportingPosition.x, _teleportingPosition.y - 1f, _teleportingPosition.z), Vector3.up, out hit, 2)) //only move when no object in the way
        {
            if (hit.collider != null)
            {
                return true;
            }
            return false;
        }
        return false;

    }
    private Vector3 GetOffsetToParent()
    {
        throw new NotImplementedException();
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
}
