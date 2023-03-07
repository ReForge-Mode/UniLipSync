using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTester : MonoBehaviour
{
    public Animator animator;
    public AudioClip audioClip;

    private AudioSource audioSource;

    private void OnValidate()
    {
        //Change the audio clip when user drag in new audio file
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    public void PlaySoundWithLipSync()
    {
        //Play the Animation STate from beginning even if it's playing
        animator.Play(audioClip.name.Substring(0, 10), 1, 0);

        //Play the Voice Over audio, restart it if needed
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.Play(0);
    }
}