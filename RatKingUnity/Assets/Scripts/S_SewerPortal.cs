using UnityEngine;

public class S_SewerPortal : MonoBehaviour
{
    [SerializeField] private S_SewerPortal _linkedPortal;
    [SerializeField] private int _radius = 2;

    private bool _skipNextTeleport = false;

    private void OnCollisionEnter(Collision collision)
    {
        //TO DO:
        // Debug.LogError("Sewer does not check if the object colliding is the player yet");
        //check if player

        if (!_skipNextTeleport)
        {
            _skipNextTeleport = true;

            //TO DO:
            //check block size 
            //Debug.LogError("Check size of the block not implemented yet");
            float blockSize = 1;

            if (blockSize > _radius)
                return;

            float forwardDistance1 = Vector3.Distance(collision.transform.position, transform.position);
            collision.transform.position = collision.transform.position - CheckPortalDistance() + (forwardDistance1 * _linkedPortal.transform.forward) - (forwardDistance1 * transform.forward);

            Quaternion rotationDifference = _linkedPortal.transform.rotation * Quaternion.Inverse(transform.rotation);
            collision.transform.rotation = rotationDifference * collision.transform.rotation;

            _linkedPortal.RecievingTeleport();
        }
        else
            _skipNextTeleport = false;
    }

    public void RecievingTeleport()
    {
        //to not teleport the player on collision in loop
        _skipNextTeleport = true;
    }
    private Vector3 CheckPortalDistance()
    {
        return transform.position - _linkedPortal.transform.position;
    }
}
