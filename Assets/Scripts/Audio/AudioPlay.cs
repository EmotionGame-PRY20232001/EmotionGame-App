using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioPlay : MonoBehaviour
{
    public bool PlayOnStart = false;

    protected AudioSource AudioSrc;
    [SerializeField]
    protected AudioManager.ESfx SfxType = AudioManager.ESfx.Regular;

    protected virtual void Awake()
    {
        AudioSrc = gameObject.GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        LoadSfx();
        if(PlayOnStart)
            Play();
    }

    protected virtual void LoadSfx()
    {
        if (AudioSrc != null && AudioManager.Instance != null)
        {
            var gmsfxs = AudioManager.Instance.Sfxs;
            if (gmsfxs.ContainsKey(SfxType))
                AudioSrc.clip = gmsfxs[SfxType];
        }
    }

    public void Play()
    {
        if (AudioSrc == null) return;
        // play once at a time
        if (!AudioSrc.isPlaying)
            AudioSrc?.Play();
    }
}
