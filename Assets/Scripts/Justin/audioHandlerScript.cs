using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioHandlerScript : MonoBehaviour
{

    AudioSource[] audioSources;

    private void Start() {
        audioSources = GetComponents<AudioSource>();
    }
    public void PlayRandomAudioSource()
    {

        // If there's no audio sources currently playing, play a random one
        if (!IsAnyAudioSourcePlaying())
        {
            PlayRandomAudioSourceFromList(4);
        }
    }

    private void PlayRandomAudioSourceFromList(int howManyAudioSourcesToPlay)
    {
        // Get a random audio source from the list
        int randomAudioSourceIndex = Random.Range(0, audioSources.Length);

        int randomIndex = Random.Range(0, audioSources.Length);
        audioSources[randomIndex].pitch = Random.Range(0.9f, 2f);
        audioSources[randomIndex].Play();

        // If we still have audio sources to play, play another one
        if (howManyAudioSourcesToPlay > 1)
        {
            PlayRandomAudioSourceFromList(howManyAudioSourcesToPlay - 1);
        }
    }

    private bool IsAnyAudioSourcePlaying()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}
