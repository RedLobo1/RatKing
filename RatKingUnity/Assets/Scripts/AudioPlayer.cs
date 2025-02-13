using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AudioPlayer : MonoBehaviour
{
    private characterMovement character;
    private SewerTeleportationReworked[] sewers;

    void Start()
    {
        character = GameObject.FindObjectOfType<characterMovement>();
        sewers = GameObject.FindObjectsOfType<SewerTeleportationReworked>();

        //TO DO: add door opening, button pressed, onsewerout, on sewerin, 
        //on detach,
        

        character.OnMove += PlayOnMove;
        character.OnConnect += PlayOnConnect;
        foreach (var sewer in sewers)
        {
            sewer.OnDisconnect += PlayOnSewerIn;
            sewer.OnSewerExit += PlayOnSewerOut;
        }
    }
    private void PlayOnConnect(object sender, PositionEventArgs e)
    {
        AudioManager.Instance.Play("PlayerConnect");
    }
    private void PlayOnMove(object sender, GrandChildrenEventArgs e)
    {
        var a = e.GrandChildren.Count;
        AudioManager.Instance.Play("Move");
        Debug.Log("step");
    }
    private void PlayOnSewerIn(object sender, PositionEventArgs e)
    {
        AudioManager.Instance.Play("SewerIn");
    }
    private void PlayOnSewerOut()
    {
        AudioManager.Instance.Play("SewerOut");
    }
    private void PlayOnButtonPressed()
    {
        AudioManager.Instance.Play("ButtonPressed");
    }
    private void PlayOnDoorOpening()
    {
        AudioManager.Instance.Play("DoorOpening");
    }
    private void PlayOnDetach()
    {
        AudioManager.Instance.Play("PlayerDetach");
    }
}
