using UnityEngine;

public interface IAudioPlayer
{ 
    public AudioSource Play(SoundData soundData, Transform spawnTransform= null, bool follow = false);
    public void Stop(AudioSource audioSource);
}