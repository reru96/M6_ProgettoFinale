using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string musicVolume = "MusicVolume";
    [SerializeField] private string sfxVolume = "SfxVolume";

    [Header("AudioSource")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Library")]
    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private SceneMusicLibrary sceneMusicLibrary;


    private Dictionary<string,AudioClip> audioDict = new Dictionary<string,AudioClip>();
    private Dictionary<string,string> sceneMusicDict = new Dictionary<string,string>();

    protected override void Awake()
    {
        base.Awake();

        if(musicSource == null)
        {
            musicSource.AddComponent<AudioSource>();
            musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            musicSource.loop = true;
        }

        if(sfxSource == null)
        {
            sfxSource.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sfx")[0];
        }

        if (audioDict != null)
        {
            foreach (var entry in audioLibrary.clips)
            {
                if (!audioDict.ContainsKey(entry.key))
                    audioDict.Add(entry.key, entry.clip);
            }
        }

        if (sceneMusicLibrary != null)
        {
            foreach (var entry in sceneMusicLibrary.sceneMusics)
            {
                if (!sceneMusicDict.ContainsKey(entry.sceneName))
                    sceneMusicDict.Add(entry.sceneName, entry.musicKey);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneMusicDict.TryGetValue(scene.name, out var musicKey))
        {
            PlayMusic(musicKey);
        }
    }

    public void PlayMusic(string key, float volume = 1f)
    {
        if (audioDict.TryGetValue(key, out var clip))
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

    public void StopMusic() => musicSource.Stop();

    public void StopSfx() => sfxSource.Stop();

    public void PlaySfx(string key, float volume = 1f)
    {
        if (audioDict.TryGetValue(key, out var clip))
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public AudioClip GetClip(string key)
    {
        if (audioDict.TryGetValue(key, out var clip))
            return clip;
        return null;
    }

    public void SetVolume(float volume, string musicGroup)
    {
        audioMixer.SetFloat(musicGroup, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }
}
