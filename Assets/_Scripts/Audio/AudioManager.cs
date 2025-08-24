using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioPlayer
{
    [SerializeField] private List<AudioSource> audioSourceTemplates;
        
    private List<AudioSource> playingSounds = new ();

    private void Awake()
    {
        ServiceLocator.Register<IAudioPlayer>(this);
    }

    public AudioSource Play(SoundData soundData, Transform spawnTransform = null, bool follow = false)
    {
        if (!soundData || !soundData.sound)
        {
            Debug.LogWarning($"No such SoundData or sound {soundData.sound.name}");
            return null;
        }
        if (this == null || gameObject == null) return null;
        if (spawnTransform == null) spawnTransform = transform;

        AudioSource audioSource = GetSuitableAudioSource(soundData);

        if (audioSource == null)
        {
            Debug.LogError($"Can't find audio source template for mixer group {audioSource.outputAudioMixerGroup}");
            return null;
        }
        
        var source = Instantiate(audioSource, spawnTransform.position, Quaternion.identity, transform);
        if (follow)
        {
            source.transform.SetParent(spawnTransform.transform);
        }
        source.gameObject.name = $"{soundData.audioMixerGroup}_{soundData.name}";
        
        source.clip = soundData.sound;
        source.volume = soundData.volume;
        source.loop = soundData.loop;
        source.outputAudioMixerGroup = soundData.audioMixerGroup;

        source.pitch = soundData.pitch;
        if (soundData.pitchVariance != 0f)
        {
            float resulPitch = Random.Range(soundData.pitch - soundData.pitchVariance, soundData.pitch + soundData.pitchVariance);
            source.pitch = resulPitch;
        }
        
        playingSounds.Add(source);
        source.Play();

        
        if (!soundData.loop)
        {
            _ = StopAfterDelay(source, source.clip.length).RunSafe();
        }

        return source;
    }

    private AudioSource GetSuitableAudioSource(SoundData soundData)
    {
        foreach (var audioSource in audioSourceTemplates)
        {
            if (audioSource.outputAudioMixerGroup == soundData.audioMixerGroup)
            {
                return audioSource;
            }
        }
        return null;
    }

    private async Awaitable StopAfterDelay(AudioSource source, float delay)
    {
        await Awaitable.WaitForSecondsAsync(delay, destroyCancellationToken);
        Stop(source);
    }
    
    public void Stop(AudioSource source)
    {
        playingSounds.Remove(source);
        if (source && source.gameObject)
        {
            Destroy(source.gameObject);
        }
    }
}
