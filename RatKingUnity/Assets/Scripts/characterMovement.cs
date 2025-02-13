using System;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    private Animator animator;

    public event EventHandler<GrandChildrenEventArgs> OnMove;
    public event EventHandler<PositionEventArgs> OnConnect;

    public CompositeCommand _command;
    public LayerMask mask;
    // Start is called before the first frame update
    Dictionary<string, Vector3> directions = new Dictionary<string, Vector3>
        {
            { "Right", new Vector3(1, 0, 0) },
            { "Left", new Vector3(-1, 0, 0) },
            { "Up", new Vector3(0, 0, 1) },
            { "Down", new Vector3(0, 0, -1) }
        };
    void Start()
    {
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CommandInvoker.Undo();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            UpdatePosition(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            UpdatePosition(directions["Left"]);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            UpdatePosition(directions["Up"]);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            UpdatePosition(directions["Down"]);
        }

    }
    public void UpdatePosition(Vector3 movementVector)
    {
        if (CanIMove(movementVector))
        {
            OnMove?.Invoke(this, GetAllGrandChildren());
            _command = new CompositeCommand();

            CheckForNeighbours(movementVector);

            _command.AddToList(new MoveCommand(movementVector, this));

            CommandInvoker.ExecuteCommand(_command);

            CheckForVisuals(movementVector);

        }
    }

    private GrandChildrenEventArgs GetAllGrandChildren()
    {
        List<GameObject> Grandchildren = new List<GameObject>();

        foreach (Transform child in this.transform)
        {
            foreach (Transform grandChild in child)
            {
                Grandchildren.Add(grandChild.gameObject);
            }
        }
        //Debug.Log(Grandchildren.Count);
        return new GrandChildrenEventArgs(Grandchildren);
    }

    public bool CanIMove(Vector3 movementVector)
    {
        //this function checks if a movement in the chosen direction would make you overlap with the wall.
        //and prevent you from moving if so

        //for player
        var targetGridPosition = this.transform.position + movementVector;
        bool canMove = !IsTargetWall(targetGridPosition);
        if (!canMove)
            return canMove; //return when canMove is false

        //for grandchilds
        foreach (Transform child in this.transform)
        {
            foreach (Transform grandChild in child)
            {
                targetGridPosition = grandChild.transform.position + movementVector;
                canMove = !IsTargetWall(targetGridPosition);
                if (!canMove)
                    return canMove; //return when canMove is false
            }
        }
        return canMove;
    }
    private void CheckForVisuals(Vector3 movementVector)
    {
        //this function checks if there is a neighbour in any direction and attaches the ones it found
        RaycastHit hit;
        foreach (Vector3 direction in directions.Values)
        {
            //self
            if (Physics.Raycast(this.transform.position, direction, out hit, 1, mask))
            {
                var blob = hit.collider.transform.parent;
                if (blob != null)
                {
                    if (blob.CompareTag("BlobDisconnected")) //Can only attach to disconnected blobs (blobs are set as connected when parented)
                    {
                        PositionEventArgs pos = new PositionEventArgs((hit.collider.gameObject.transform.position + this.transform.position) / 2f, hit.collider.transform.parent.gameObject); //average position is the connection point
                        TriggerVisualsCommand command = new TriggerVisualsCommand(this, pos);
                        CommandInvoker.ExecuteCommand(command);
                    }
                }
                //AttachNeighbours(hit);
            }

            //grandchilds
            foreach (Transform child in this.transform)
            {
                foreach (Transform grandChild in child) //sendRayFromGrandChilds as well
                {
                    if (Physics.Raycast(grandChild.transform.position, direction, out hit, 1, mask))
                    {
                        var blob = hit.collider.transform.parent;
                        if (blob != null)
                        {
                            if (blob.CompareTag("BlobDisconnected")) //Can only attach to disconnected blobs (blobs are set as connected when parented)
                            {
                                PositionEventArgs pos = new PositionEventArgs((hit.collider.gameObject.transform.position + grandChild.transform.position) / 2f, hit.collider.transform.parent.gameObject); //average position is the connection point
                                TriggerVisualsCommand command = new TriggerVisualsCommand(this, pos);
                                CommandInvoker.ExecuteCommand(command);
                            }
                        }
                        //AttachNeighbours(hit);
                    }
                }
            }
        }
        //CommandInvoker.ExecuteCommand(_command);
    }

    public void CallVisualEvent(PositionEventArgs pos)
    {
        OnConnect?.Invoke(this, pos);
    }

    private void CheckForNeighbours(Vector3 movementVector)
    {
        //this function checks if there is a neighbour in any direction and attaches the ones it found
        RaycastHit hit;
        foreach (Vector3 direction in directions.Values)
        {
            //self
            if (Physics.Raycast(this.transform.position, direction, out hit, 1, mask))
            {
                var blob = hit.collider.transform.parent;
                if (blob != null)
                {
                    if (blob.CompareTag("BlobDisconnected")) //Can only attach to disconnected blobs (blobs are set as connected when parented)
                    {
                        //Debug.Log("Blob touched");
                        _command.AddToList(new AttachCommand(this, hit));
                    }
                }
                //AttachNeighbours(hit);
            }

            //grandchilds
            foreach (Transform child in this.transform)
            {
                foreach (Transform grandChild in child) //sendRayFromGrandChilds as well
                {
                    if (Physics.Raycast(grandChild.transform.position, direction, out hit, 1, mask))
                    {
                        var blob = hit.collider.transform.parent;
                        if (blob != null)
                        {
                            if (blob.CompareTag("BlobDisconnected")) //Can only attach to disconnected blobs (blobs are set as connected when parented)
                            {
                                _command.AddToList(new AttachCommand(this, hit));
                            }
                        }
                        //AttachNeighbours(hit);
                    }
                }
            }
        }
        //CommandInvoker.ExecuteCommand(_command);
    }

    private void AttachNeighbours(RaycastHit hit)
    {
        var blob = hit.collider.transform.parent;
        if (blob != null)
        {
            if (blob.CompareTag("BlobDisconnected")) //Can only attach to disconnected blobs (blobs are set as connected when parented)
            {
                //Debug.Log("Touching Blob");
                blob.SetParent(this.transform); //only do this is this has not been done before
                blob.tag = "BlobConnected";
            }
        }
    }

    bool IsTargetWall(Vector3 targetGridPosition)
    {
        Vector3 rayOrigin = new Vector3(targetGridPosition.x, targetGridPosition.y + 0.5f, targetGridPosition.z);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector2.down, out hit, 2))
        {
            if (hit.collider != null)
            {
                var tileType = hit.collider.GetComponent<TileProperties>();
                if (tileType.TileType == ETileType.Wall)
                {
                    //Debug.Log("Cannot move to wall");
                    return true;
                }
            }
        }
        return false;
    }
}
