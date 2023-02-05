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
            PlayRandomAudioSourceFromList(4, true);
        }
    }

    public void PlayRandomAudioSourceFromList(int howManyAudioSourcesToPlay, bool isChangingPitch)
    {
        // Get a random audio source from the list
        int randomAudioSourceIndex = Random.Range(0, audioSources.Length);

        int randomIndex = Random.Range(0, audioSources.Length);
        audioSources[randomIndex].Play();

        if(isChangingPitch == true)
        {
            audioSources[randomIndex].pitch = Random.Range(0.9f, 2f);
        }

        // If we still have audio sources to play, play another one
        if (howManyAudioSourcesToPlay > 1)
        {
            PlayRandomAudioSourceFromList(howManyAudioSourcesToPlay - 1, true);
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

    public void PlaySpecificAudioSource(int audioSourceIndex)
    {
      audioSources[audioSourceIndex].Play();
    }
}
