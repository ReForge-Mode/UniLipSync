using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTester : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Play"))
        {
            anim.SetTrigger("A");
            audioData.Play(0);
        }
    }
}
