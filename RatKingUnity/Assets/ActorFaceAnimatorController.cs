using UnityEngine;

public class ActorFaceAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator faceAnimator;
    [SerializeField] private characterMovement character;

    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<characterMovement>();
        if (character != null)
        {
            character.OnConnect += PlayConnectAnimation;
            character.OnMove += PlayMoveAnimation;
        }
    }
    private void PlayMoveAnimation(object sender, GrandChildrenEventArgs e)
    {
        if (faceAnimator != null)
        {

                Transform parentTransform = transform.parent;
                Transform grandParent = parentTransform.parent;

                if (grandParent.tag == "BlobConnected")
                {
                faceAnimator.Play("isMoving");
                }

        }
    }
    private void PlayConnectAnimation(object sender, PositionEventArgs e)
    {
        if (faceAnimator != null)
        {
            GameObject currentConnectedGameObject = e.Entity; // Get the connected entity (blob)

            if (currentConnectedGameObject != null)
            {
                foreach (Transform child in currentConnectedGameObject.transform) // Iterate through its children
                {
                    Transform grandChild = child.GetChild(0);
                    if (grandChild.gameObject == this.gameObject) // Check if this script's GameObject is a child
                    {
                        faceAnimator.Play("isConnected");
                        return; // Exit after finding a match
                    }
                }
            }
        }
    }
}
