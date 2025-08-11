using UnityEngine;

public class DebugAudioPlayer : IAudioPlayer
{
    public void Play(AudioClip clip, Vector3 position,  float volume = 1f)
    {
        Debug.Log("DbgAudioPlayer: Playing " + clip?.name + " at " + position);
    }

    public AudioSource Play(SoundData soundData, Transform spawnTransform, bool follow = false)
    {
        Debug.Log("DbgAudioPlayer: Playing " + soundData?.name + " at " + spawnTransform.position);
        return null;
    }

    public void Stop(AudioSource audioSource)
    {
        Debug.Log($"DbgAudioPlayer: Stop Playing {audioSource?.name}");
    }
}