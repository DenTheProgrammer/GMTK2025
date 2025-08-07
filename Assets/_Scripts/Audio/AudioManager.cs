using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioPlayer
{
    [SerializeField] private List<AudioSource> audioSourceTemplates;
        
    private List<AudioSource> playingSounds = new ();

    private void Awake()
    {
        ServiceLocator.Register(this, false);
    }

    public AudioSource Play(SoundData soundData, Vector3 position)
    {
        if (!soundData || !soundData.sound)
        {
            Debug.LogWarning($"No such SoundData or sound {soundData.sound.name}");
            return null;
        }
        if (this == null || gameObject == null || transform == null) return null;

        AudioSource audioSource = GetSuitableAudioSource(soundData);

        if (audioSource == null)
        {
            Debug.LogError($"Can't find audio source template for mixer group {audioSource.outputAudioMixerGroup}");
            return null;
        }
        
        var source = Instantiate(audioSource, position, Quaternion.identity, transform);
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
            StartCoroutine(StopAfterDelay(source, source.clip.length));
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

    private IEnumerator StopAfterDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        StopSound(source);
    }
    
    public void StopSound(AudioSource source)
    {
        playingSounds.Remove(source);
        if (source && source.gameObject)
        {
            Destroy(source.gameObject);
        }
    }
}
