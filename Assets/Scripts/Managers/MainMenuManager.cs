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

    [SerializeField]
    protected float timePerStar = 0.5f;
    protected uint numStars = 0;
    public static sbyte LastStarsWon = 0;

    protected void Start()
    {
        var gm = GameManager.Instance;

        if (gm != null && gm.IsPlayerActive())
        {
            var player = gm.GetCurrentPlayer();

            if (PlayerGreeting != null)
                PlayerGreeting.text = GreetingText.Replace("$", player.Name);

            numStars = player.StarsWon - (uint)LastStarsWon;
            UpdateStars();

            if (LastStarsWon > 0)
            {
                StartCoroutine(Countdown());
            }
        }
    }

    protected void UpdateStars()
    {
        if (Stars != null)
        {
            Stars.text = numStars.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(StarsLayout);
        }
    }

    System.Collections.IEnumerator Countdown()
    {
        while (LastStarsWon > 0)
        {
            numStars++;
            LastStarsWon--;
            UpdateStars();
            yield return new WaitForSeconds(timePerStar);
        }
    }
}
