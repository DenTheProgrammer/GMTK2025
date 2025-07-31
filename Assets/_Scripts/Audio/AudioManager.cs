using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioPlayer
{
    public AudioLibrarySO audioLibrary; 
        
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

        var sourceGO = new GameObject("_Audio");
        sourceGO.transform.SetParent(transform);
        sourceGO.transform.position = position;

        var source = sourceGO.AddComponent<AudioSource>();
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
        
        source.spatialBlend = 1f;
        source.rolloffMode = AudioRolloffMode.Logarithmic; 
        source.minDistance = 1f;
        source.maxDistance = 500f;
        source.spread = 0f;
        source.dopplerLevel = 1f;
        
        playingSounds.Add(source);
        source.Play();

        
        if (!soundData.loop)
        {
            StartCoroutine(StopAfterDelay(source, source.clip.length));
        }

        return source;
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
