using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControllerAndroid : MonoBehaviour
{
    public static AudioSourceControllerAndroid current;

    public AudioSource Player;
    public AudioSource Effects;
    public AudioSource Environment;
    public AudioSource Music;

    private void Awake()
    {
        current = this;
    }
}
