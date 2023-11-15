using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject playerButtonPrefab;

    public void ShowPlayers(List<Player> players)
    {
        foreach (Player player in players)
        {
            var instance = Instantiate(playerButtonPrefab, content.transform);
            instance.GetComponent<PlayerButton>().ButtonInstantiate(player);
        }
    }
}
