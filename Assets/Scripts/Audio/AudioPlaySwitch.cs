using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioPlaySwitch : AudioPlay
{
    protected bool isOn = false;
    [SerializeField]
    protected AudioManager.ESfx SfxTypeOff = AudioManager.ESfx.Off;

    protected override void Awake()
    {
        base.Awake();

        Toggle toggle = gameObject.GetComponent<Toggle>();
        if (toggle != null)
        {
            isOn = toggle.isOn;
            toggle.onValueChanged.AddListener(delegate{ PlayOnSwitch(toggle); });
        }

    }

    protected override void LoadSfx()
    {
        if (AudioSrc != null && AudioManager.Instance != null)
        {
            var gmsfxs = AudioManager.Instance.Sfxs;
            AudioManager.ESfx sfxType = isOn ? SfxType : SfxTypeOff;
            if (gmsfxs.ContainsKey(sfxType))
                AudioSrc.clip = gmsfxs[sfxType];
        }
    }

    public virtual void Play(bool isOn)
    {
        this.isOn = isOn;
        LoadSfx();
        AudioSrc?.Play();
    }

    public void PlayOnSwitch(Toggle toggle)
    {
        Play(toggle.isOn);
    }
}
