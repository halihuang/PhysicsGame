using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BlackLoadCover : MonoBehaviour
{
    // director of the scene
    [SerializeField] private PlayableDirector fadeToBlackDirector;
    [SerializeField] private PlayableDirector fadeFromBlackDirector;

    public void FadeToBlack()
    {
        fadeToBlackDirector.Play();
    }

    public void FadeFromBlack()
    {
        fadeFromBlackDirector.Play();
    }
}
