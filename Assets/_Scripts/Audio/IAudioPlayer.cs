using UnityEngine;

public interface IAudioPlayer
{ 
    public AudioSource Play(SoundData soundData, Transform spawnTransform, bool follow = false);
    public void Stop(AudioSource audioSource);
}