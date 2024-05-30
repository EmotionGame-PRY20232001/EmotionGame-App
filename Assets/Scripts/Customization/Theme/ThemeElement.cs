using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeElement : MonoBehaviour
{
    [SerializeField]
    protected bool UseFromPlayer = false;

    public enum EElement { Background, Frame, SpeechTail, Mirror };
    [SerializeField]
    protected EElement ElementType;

    [field:SerializeField]
    public Theme.EBackground Id { get; protected set; }

    protected virtual void Awake() { }
    protected virtual void Start()
    {
        if (UseFromPlayer)
            Id = GetCurrentPlayerId();
        UpdateThemeElement();
    }

    protected virtual Theme.CustomBackground GetBackgroundData()
    {
        if (GameManager.Instance == null) return new Theme.CustomBackground();
        return GameManager.Instance.GetBackgrounds()[Id];
    }

    protected virtual void UpdateThemeElement() { }

    public void UpdateThemeElement(Theme.EBackground id)
    {
        Id = id;
        UpdateThemeElement();
    }

    public static Theme.EBackground GetCurrentPlayerId()
    {
        Theme.EBackground id = Theme.EBackground.Main;

        if (GameManager.Instance)
        {
            var player = GameManager.Instance.GetCurrentPlayer();
            if (player != null && player.Id != -1)
                id = (Theme.EBackground)player.BackgroundId;
        }

        return id;
    }
}
