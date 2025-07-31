using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundData")]
public class SoundData : ScriptableObject
{
    public AudioClip sound;
    public AudioMixerGroup audioMixerGroup;
    public float volume = 1f;
    public bool loop = false;
    public float pitch = 1f;
    public float pitchVariance = 0f;
}
