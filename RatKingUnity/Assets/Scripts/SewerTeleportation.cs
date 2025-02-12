using UnityEngine;

public class SewerTeleportation : MonoBehaviour
{
    [SerializeField]
    private GameObject _TargetTile;

    [SerializeField]
    private GameObject _connectedSewer;
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
                Debug.Log("Teleporting");
                hit.collider.transform.position = new Vector3(_TargetTile.transform.position.x, hit.collider.transform.position.y, _TargetTile.transform.position.z);
                float RotationAngle = Vector3.Angle(this.transform.forward, _connectedSewer.transform.forward);
                hit.collider.transform.rotation = Quaternion.Euler(0, hit.collider.transform.rotation.y+RotationAngle, 0); //Rotating the object
            }
        }
    }
}
