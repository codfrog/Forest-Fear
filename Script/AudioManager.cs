using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;

    public AudioClip Audio1;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
