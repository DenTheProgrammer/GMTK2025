using UnityEngine;

public interface IAudioPlayer
{ 
    public AudioSource Play(SoundData soundData, Vector3 position);
    public void Stop(AudioSource audioSource);
}