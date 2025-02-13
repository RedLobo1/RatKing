using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBodyAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private characterMovement character;

    void Start()
    {
        // Automatically find the character if not assigned
        if (character == null)
        {
            character = FindObjectOfType<characterMovement>();
        }

        if (character != null)
        {
            character.OnMove += PlayActorMoveAnimation;
        }
        else
        {
            Debug.LogWarning("CharacterMovement component not found in the scene!");
        }
    }

    private void PlayActorMoveAnimation(object sender, GrandChildrenEventArgs e)
    {
        
        if (e.GrandChildren == null) return; 

        if (e.GrandChildren.Contains(gameObject))
        {
            MoveActor();
        }
    }

    private void MoveActor()
    {
        if (animator != null)
        {
            animator.Play("isMoving");
        }
    }

    void OnDestroy()
    {
        if (character != null)
        {
            character.OnMove -= PlayActorMoveAnimation; // Clean up event subscription
        }
    }
}

