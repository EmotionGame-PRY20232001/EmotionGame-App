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
        ExerciseImage.texture = GameManager.Instance.Emotions[ExerciseEmotion].Faces[0].texture;
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
