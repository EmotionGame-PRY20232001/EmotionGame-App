using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private int Score;

    [SerializeField]
    private GameObject AreaOfButtons;
    [SerializeField]
    private EmotionButton BtnEmotionPrefab;

    [SerializeField]
    public Emotion.EEmotion ExerciseEmotion;

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
        var gm = GameManager.Instance;
        var se = new List<Emotion.EEmotion>(gm.SelectedEmotions);
        Emotion.EEmotion randEmotion = se[Random.Range(0, se.Count)];
        ExerciseEmotion = randEmotion;
        for (int i = 0; i < 3; i++)
        {
            var es = gm.Emotions[randEmotion].Sprite;
            var bt = Instantiate(BtnEmotionPrefab, AreaOfButtons.transform);
            bt.SetButton(es);
            se.Remove(randEmotion);
            randEmotion = se[Random.Range(0, se.Count)];
        }
        var fl = gm.Emotions[ExerciseEmotion].Faces;
        ExerciseImage.texture = fl[Random.Range(0, fl.Count)].texture;
    }

    private void StartContextActivity()
    {

    }

    private void StartImitateActivity()
    {

    }

    public void Good()
    {
        Score += 10;
        UpdateScoreText();
    }
    
    public void Bad()
    {
        Score -= 5;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + Score;
    }
}
