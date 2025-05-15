using UnityEngine.UI;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    protected TMPro.TMP_Text PlayerGreeting;
    [SerializeField]
    protected RectTransform StarsLayout;
    [SerializeField]
    protected TMPro.TMP_Text Stars;
    [SerializeField]
    protected string GreetingText;

    protected void Start()
    {
        var gm = GameManager.Instance;

        if (gm != null && gm.IsPlayerActive())
        {
            var player = gm.GetCurrentPlayer();

            if (PlayerGreeting != null)
                PlayerGreeting.text = GreetingText.Replace("$", player.Name);

            if (Stars != null)
            {
                Stars.text = player.StarsWon.ToString();
                LayoutRebuilder.ForceRebuildLayoutImmediate(StarsLayout);
            }
        }
    }
}
