using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGuide : Character
{
    private void Start()
    {
        LoadDefault();
    }

    public void LoadDefault()
    {
        if (GameManager.Instance)
        {
            var player = GameManager.Instance.GetCurrentPlayer();
            SetByJson(player.GuideJSON);
        }
    }
}
