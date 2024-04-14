using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActivityManager : EmotionExercise
{
    private static ActivityManager _instance;
    public static ActivityManager Instance {  get { return _instance; } }

    private enum EActivity { Choose, Context, Imitate }

    [SerializeField] 
    private EActivity Activity;
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text ExerciseText;
    [SerializeField] 
    private int NumButtons = 4;
    [SerializeField]
    private int Score;

    [SerializeField]
    private GameObject AreaOfButtons;
    [SerializeField]
    private GameObject GridOfButtons;
    [SerializeField]
    private EmotionButton BtnEmotionPrefab;
    [SerializeField]
    private EmotionButton BtnEmotionMovePrefab;


    [SerializeField]
    private FERModel Model;

    private List<GameObject> InstantiateButtons = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        CurrentExercise = -1;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        SetEmotionExercises();
        UpdateScoreText();
        LoadExercise();
    }

    private void CleanUp()
    {
        foreach (var go in InstantiateButtons) Destroy(go);
    }

    public void Good()
    {
        Score += 10;
        UpdateScoreText();
        LoadExercise();
    }
    
    public void Bad()
    {
        if (Score >= 5) Score -= 5;
        UpdateScoreText();
        LoadExercise();
    }

    void LoadExercise()
    {
        LoadCurrentExercise(CurrentExercise + 1);
        switch (Activity)
        {
            case EActivity.Choose:
                LoadChooseExercise();
                break;
            case EActivity.Context:
                LoadContextExercise();
                break;
            case EActivity.Imitate:
                LoadImitateExercise();
                break;
            default: break;
        }
    }

    protected override void LoadExercise(EmotionPhoto photo)
    {
        base.LoadExercise(photo);
        photo?.gameObject.SetActive(false);
    }

    protected virtual void EnableCurrentExercise (bool value)
    {
        if (Exercises == null || CurrentExercise < 0) return;

        if (Exercises.Photos.Count > CurrentExercise &&
            Exercises.Photos[CurrentExercise] != null)
        {
            Exercises.Photos[CurrentExercise].gameObject.SetActive(value);
        }
    }

    protected virtual void LoadCurrentExercise(int newCurrent)
    {
        EnableCurrentExercise(false);
        CurrentExercise = newCurrent;
        EnableCurrentExercise(true);
        LoadCurrentEmotion();
    }

    protected virtual void LoadEmotionButtons(EmotionButton btnEmoionPrefab)
    {
        CleanUp();

        var gm = GameManager.Instance;
        var allEmotions = new List<Emotion.EEmotion>(gm.AllEmotions);
        allEmotions.Remove(ExerciseEmotion);
        allEmotions = allEmotions.OrderBy(x => Random.value).ToList();

        var selEmotions = allEmotions.GetRange(0, NumButtons - 1);
        selEmotions.Add(ExerciseEmotion);
        selEmotions = selEmotions.OrderBy(x => Random.value).ToList();
        
        foreach (Emotion.EEmotion emotion in selEmotions)
        {
            var button = Instantiate(btnEmoionPrefab, AreaOfButtons.transform);
            button.SetEmotion(emotion);
            InstantiateButtons.Add(button.gameObject);
        }
    }

    private void LoadChooseExercise()
    {
        LoadEmotionButtons(BtnEmotionMovePrefab);
    }

    private void LoadContextExercise()
    {
        LoadEmotionButtons(BtnEmotionPrefab);

        var gm = GameManager.Instance;
        var contextStrings = gm.Emotions[ExerciseEmotion].Contexts;
        ExerciseText.text = contextStrings[Random.Range(0, contextStrings.Count)];
    }

    private void LoadImitateExercise()
    {
        StartCoroutine(CheckImitateEmotion());
    }

    IEnumerator CheckImitateEmotion()
    {
        while (true)
        {
            if (Model.PredictedEmotion == ExerciseEmotion)
            {
                Debug.Log("Sí es");
                LoadImitateExercise();
            }
            yield return null;
        }
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + Score;
    }
}
