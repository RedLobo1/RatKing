using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AudioPlayer : MonoBehaviour
{
    private characterMovement character;

    void Start()
    {
        character = GameObject.FindObjectOfType<characterMovement>();

        //TO DO: add door opening, button pressed, onsewerout, on sewerin, 
        //on detach,
        
        character.OnMove += PlayOnMove;
        character.OnConnect += PlayOnConnect;
    }
    private void PlayOnConnect(object sender, PositionEventArgs e)
    {
        AudioManager.Instance.Play("PlayerConnect");
    }
    private void PlayOnMove()
    {
        AudioManager.Instance.Play("Move");
        Debug.Log("step");
    }
    private void PlayOnSewerIn()
    {
        AudioManager.Instance.Play("SewerIn");
    }
    private void PlayOnButtonPressed()
    {
        AudioManager.Instance.Play("ButtonPressed");
    }
    private void PlayOnDoorOpening()
    {
        AudioManager.Instance.Play("DoorOpening");
    }
    private void PlayOnSewerOut()
    {
        AudioManager.Instance.Play("SewerOut");
    }
    private void PlayOnDetach()
    {
        AudioManager.Instance.Play("PlayerDetach");
    }
}
