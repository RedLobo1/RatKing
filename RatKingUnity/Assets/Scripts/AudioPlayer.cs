using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AudioPlayer : MonoBehaviour
{
    private characterMovement character;
    private SewerTeleportationReworked[] sewers;
    private Button[] buttons;
    //private Laser[] lasers;

    void Start()
    {
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;

        FindAllObjectsOfType();
        PlayAmbiance();

    }
    private void Update()
    {
        if (character == null)
        {
            FindAllObjectsOfType();
            PlayOnNextLevel();
        }
    }
    private void FindAllObjectsOfType()
    {
        character = GameObject.FindObjectOfType<characterMovement>();
        sewers = GameObject.FindObjectsOfType<SewerTeleportationReworked>();
        buttons = GameObject.FindObjectsOfType<Button>();
        //lasers = GameObject.FindObjectsOfType<Laser>();


        //TO DO: add events
        character.OnMove += PlayOnMove;
        character.OnConnect += PlayOnConnect;

        //character.on += PlayOnPlayerLanding;
        character.OnMoveReverse += PlayOnReverse;

        foreach (var sewer in sewers)
        {
            sewer.OnDisconnect += PlayOnSewerIn;
            sewer.OnSewerExit += PlayOnSewerOut;
        }
        foreach (var button in buttons)
        {
            button.OnButtonPressed += PlayOnButtonPressed;
            button.OnDoorUnlock += PlayOnDoorOpening;
        }
    }

    private void PlayOnPlayerLanding()
    {
        AudioManager.Instance.Play("LandingSplash");
    }
    private void PlayOnNextLevel()
    {
        AudioManager.Instance.Play("NextLevel");
    }
    private void PlayOnReverse()
    {
        AudioManager.Instance.Play("ReverseMove");
    }
    private void PlayAmbiance()
    {
        AudioManager.Instance.Play("Ambiance");
        AudioManager.Instance.Play("Music");

    }
    private void PlayOnConnect(object sender, PositionEventArgs e)
    {
        AudioManager.Instance.Play("PlayerConnect");
    }
    private void PlayOnMove(object sender, GrandChildrenEventArgs e)
    {
        float a = 0;
        //a = e.GrandChildren.Count;
        AudioManager.Instance.Play("Move", a);
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
    private void PlayOnLaserFiring()
    {
        AudioManager.Instance.Play("LaserFiring");
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
