using UnityEngine;

public class characterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpdatePosition(new Vector3(1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UpdatePosition(new Vector3(-1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdatePosition(new Vector3(0, 0, 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UpdatePosition(new Vector3(0, 0, -1));
        }
    }
    void UpdatePosition(Vector3 movementVector)
    {
        var targetGridPosition = this.transform.position + movementVector;
        if (!IsTargetWall(targetGridPosition))
        {
            this.transform.position = targetGridPosition;
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
                Debug.Log("Checking");
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
