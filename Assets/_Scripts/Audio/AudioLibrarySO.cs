using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Scriptable Objects/AudioLibrary")]
public class AudioLibrarySO : ScriptableObject
{
    [Header("Music")]
    public SoundData bgMusic;
    
    [Header("SFX")]
    public SoundData click;
    public SoundData levelCompleted;
}
