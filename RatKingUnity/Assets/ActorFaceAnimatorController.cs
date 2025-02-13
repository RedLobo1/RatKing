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
            faceAnimator.Play("isMoving");
        }
    }
    private void PlayConnectAnimation(object sender, PositionEventArgs e)
    {
        if (faceAnimator != null)
        {
            faceAnimator.Play("isConnected");

        }
    }
}
