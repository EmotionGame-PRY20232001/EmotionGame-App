using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentBackground : CustomBackground
{
    protected override void Start()
    {
        base.Start();
        LoadDefault();
    }

    public void LoadDefault()
    {
        if (GameManager.Instance)
        {
            Theme.EBackground id = Theme.EBackground.Main;
            var player = GameManager.Instance.GetCurrentPlayer();
            if (player != null && player.Id != -1)
                id = (Theme.EBackground)player.BackgroundId;
            
            ChangeBackground(id);
        }
    }
}
