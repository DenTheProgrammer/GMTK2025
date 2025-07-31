using UnityEngine;

public class DebugAudioPlayer : IAudioPlayer
{
    public void Play(AudioClip clip, Vector3 position,  float volume = 1f)
    {
        Debug.Log("DbgAudioPlayer: Playing " + clip?.name + " at " + position);
    }

    public AudioSource Play(SoundData soundData, Vector3 position)
    {
        Debug.Log("DbgAudioPlayer: Playing " + soundData?.name + " at " + position);
        return null;
    }
}