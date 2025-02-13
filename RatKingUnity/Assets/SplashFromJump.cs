using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashFromJump : MonoBehaviour
{

    public void PlaySplash()
    {
        AudioManager.Instance.Play("LandingSplash");
    }

}
