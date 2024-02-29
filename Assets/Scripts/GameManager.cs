using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    private Player currentPlayer;
    [SerializeField]
    private List<Texture2D> backgrounds;
    [SerializeField]
    private List<Sprite> guideSprites;
    [SerializeField]
    private List<string> backgroundNames;
    [SerializeField]
    private List<Sprite> emotionSprites;

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
        Application.targetFrameRate = 60;
    }

    public void ChangeBackground(int backgroundId)
    {
        GameObject.Find("Background").GetComponent<RawImage>().texture = backgrounds[backgroundId];
    }

    public void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public List<Texture2D> GetBackgrounds()
    {
        return backgrounds;
    }

    public List<Sprite> GetGuideSprites()
    {
        return guideSprites;
    }

    public List<string> GetBackgroundNames() 
    {
        return backgroundNames;
    }

    public List<Sprite> GetEmotionSprites()
    {
        return emotionSprites;
    }
}
