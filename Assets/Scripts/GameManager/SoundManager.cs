using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!Application.isPlaying) return;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            PlaySound(SoundType.LEVEL, 1);
        }
    }

    public void PlaySound(SoundType sound, float volume = 1f)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

    #if UNITY_EDITOR
    private void OnValidation()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }

    }
    #endif
}

[Serializable]
public struct SoundList
{
 
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;   
    public AudioClip[] Sounds { get => sounds; }
}