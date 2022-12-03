using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTester : MonoBehaviour
{
    public Animator anim;
    public AudioClip audioClip;
    private AudioSource audioSource;

    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Play"))
        {
            //anim.SetTrigger("A");
            anim.Play(audioClip.name.Substring(0, 10));
            audioSource.Play(0);
        }
    }
}
