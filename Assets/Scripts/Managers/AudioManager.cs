using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [field: SerializeField]
    [SerializedDictionary("Tag", "AudioClip")]
    public SerializedDictionary<ESfx, AudioClip> Sfxs { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        var gm = GameManager.Instance;
        if (gm != null && gm.IsPlayerActive())
        {
            AudioListener.pause = !gm.currentPlayer.EnableAudio;
        }
        else
        {
            AudioListener.pause = false;
        }
    }

    public void EnableAudio(Toggle toggle)
    {
        var gm = GameManager.Instance;
        if (gm != null)
        {
            AudioListener.pause = !toggle.isOn;

            if (gm.IsPlayerActive())
                gm.currentPlayer.EnableAudio = toggle.isOn;
        }
    }

    public enum ESfx
    {
        None,
        // Button
        Regular,
        Accept,
        Cancel,
        Correct,
        Fail,
        Add,
        Remove,
        Invalid,
        Selection,
        On,
        Off,
        // Game
        StartGame,
        AddScore,
        Star,
        OpenPopUp,
        ClosePopUp,
        Win,
        CameraShutter,
        BubbleCollision,
    }

}
