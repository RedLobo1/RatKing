using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpdatePosition(directions["Right"]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UpdatePosition(directions["Left"]);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdatePosition(directions["Up"]);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UpdatePosition(directions["Down"]);
        }
    }
    void UpdatePosition(Vector3 movementVector)
    {
        var targetGridPosition = this.transform.position + movementVector;
        if (!IsTargetWall(targetGridPosition))
        {
            this.transform.position = targetGridPosition;
            AttachNeighbours(movementVector);
        }
    }

    private void AttachNeighbours(Vector3 movementVector)
    {
        //this function checks if there is a neighbour in any direction and attaches the ones it found
        RaycastHit hit;
        foreach (Vector3 direction in directions.Values)
        {
            if (Physics.Raycast(this.transform.position, direction, out hit, 1))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Blob"))
                    {
                        Debug.Log("Touching Blob");
                        hit.collider.transform.parent.SetParent(this.transform);
                    }
                }
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
                    Debug.Log("Cannot move to wall");
                    return true;
                }
            }
        }
        return false;
    }
}
