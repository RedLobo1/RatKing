using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private characterMovement character;
    void Start()
    {
        character = GameObject.FindObjectOfType<characterMovement>();
        character.OnMove += PlayOnMove;
        character.OnConnect += PlayOnConnect;

    }

    private void PlayOnConnect(object sender, PositionEventArgs e)
    {
        //Debug.Log("Connection Sound");
    }

    private void PlayOnMove(object sender, GrandChildrenEventArgs e)
    {
        //Debug.Log("Moving Sound");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
