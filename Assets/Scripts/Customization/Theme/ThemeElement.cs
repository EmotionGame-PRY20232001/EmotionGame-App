using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeElement : MonoBehaviour
{
    [SerializeField]
    protected bool UseFromPlayer = false;

    [field:SerializeField]
    public Theme.EBackground Id { get; protected set; }

    protected virtual void Start()
    {
        if (UseFromPlayer)
            Id = GetFromCurrentPlayer();
        UpdateThemeElement();
    }

    protected virtual void UpdateThemeElement() { }

    public void UpdateThemeElement(Theme.EBackground id)
    {
        Id = id;
        UpdateThemeElement();
    }
    public static Theme.EBackground GetFromCurrentPlayer()
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
