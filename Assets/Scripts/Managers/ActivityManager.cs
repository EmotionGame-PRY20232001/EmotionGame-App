using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour
{
    private static ActivityManager _instance;
    public static ActivityManager Instance {  get { return _instance; } }

    private enum EActivity { Choose, Context, Imitate }

    [SerializeField] 
    private EActivity Activity;
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField] 
    private RawImage ExerciseImage;
    [SerializeField] 
    private int NumButtons = 4;
    [SerializeField]
    private int Score;

    [SerializeField]
    private GameObject AreaOfButtons;
    [SerializeField]
    private EmotionButton BtnEmotionPrefab;

    [SerializeField]
    public Emotion.EEmotion ExerciseEmotion { get; private set; }

    private List<GameObject> InstantiateButtons = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score: " + Score;
        int cant = System.Enum.GetNames(typeof(Emotion.EEmotion)).Length;
        ExerciseEmotion = (Emotion.EEmotion)Random.Range(1, cant - 1);
        switch (Activity)
        {
            case EActivity.Choose: StartChooseActivity(); break;
            case EActivity.Context: StartContextActivity(); break;
            case EActivity.Imitate: StartImitateActivity(); break;
            default: break;
        }
    }

    private void StartChooseActivity()
    {
        LoadChooseExcercise();
    }

    private void StartContextActivity()
    {

    }

    private void StartImitateActivity()
    {

    }

    private void CleanUp()
    {
        foreach (var go in InstantiateButtons) Destroy(go);
    }

    public void Good()
    {
        Score += 10;
        UpdateScoreText();
        LoadChooseExcercise();
    }
    
    public void Bad()
    {
        if (Score >= 5) Score -= 5;
        UpdateScoreText();
        LoadChooseExcercise();
    }

    private void LoadChooseExcercise()
    {
        CleanUp();
        var gm = GameManager.Instance;
        var allEmotions = new List<Emotion.EEmotion>(gm.AllEmotions);
        var selEmotions = new List<Emotion.EEmotion>(gm.SelectedEmotions);
        for (int i = 0; i < NumButtons; i++)
        {
            Emotion.EEmotion randEmotion = allEmotions[Random.Range(0, allEmotions.Count)];
            if (i == 0)
            {
                randEmotion = selEmotions[Random.Range(0, selEmotions.Count)];
                ExerciseEmotion = randEmotion;
            }
            var emoSprite = gm.Emotions[randEmotion].Sprite;
            var button = Instantiate(BtnEmotionPrefab, AreaOfButtons.transform);
            button.SetButton(emoSprite, randEmotion);
            allEmotions.Remove(randEmotion);
            InstantiateButtons.Add(button.gameObject);
        }
        var faceImages = gm.Emotions[ExerciseEmotion].Faces;
        ExerciseImage.texture = faceImages[Random.Range(0, faceImages.Count)].texture;
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + Score;
    }
}
