using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private characterMovement character;

    void Start()
    {
        character = FindObjectOfType<characterMovement>();
        if (character != null)
        {
            character.OnMove += PlayMoveAnimation;
        }
    }

    private void PlayMoveAnimation(object sender, GrandChildrenEventArgs e)
    {
        if (animator != null)
        {
            animator.Play("isMoving");
            //Debug.Log("Moved");
        }
    }
    void OnDestroy()
    {
        // Unsubscribe when object is destroyed to prevent memory leaks
        if (character != null)
        {
            character.OnMove -= PlayMoveAnimation;
        }
    }
}

