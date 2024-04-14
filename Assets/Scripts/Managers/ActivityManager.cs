using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private TMP_Text ExerciseText;
    [SerializeField] 
    private EmotionPhoto ExerciseImage;
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
    public Emotion.EEmotion ExerciseEmotion { get; private set; }


    [SerializeField]
    private FERModel Model;

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
        switch (Activity)
        {
            case EActivity.Choose: LoadChooseExercise(); break;
            case EActivity.Context: LoadContextExercise(); break;
            case EActivity.Imitate: 
                LoadImitateExercise();
                StartCoroutine(CheckImitateEmotion());
                break;
            default: break;
        }
    }

    private void LoadChooseExercise()
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
            var button = Instantiate(BtnEmotionMovePrefab, AreaOfButtons.transform);
            button.SetEmotion(randEmotion);
            allEmotions.Remove(randEmotion);
            InstantiateButtons.Add(button.gameObject);
        }
        if (ExerciseImage != null)
            ExerciseImage.SetPhotoEmotion(ExerciseEmotion);
    }

    private void LoadContextExercise()
    {
        CleanUp();
        var gm = GameManager.Instance;
        var allEmotions = new List<Emotion.EEmotion>(gm.AllEmotions);
        var selEmotions = new List<Emotion.EEmotion>(gm.SelectedEmotions);
        var intEmotions = new List<Emotion.EEmotion>();
        for (int i = 0; i < NumButtons; i++)
        {
            Emotion.EEmotion randEmotion = allEmotions[Random.Range(0, allEmotions.Count)];
            if (i == 0)
            {
                randEmotion = selEmotions[Random.Range(0, selEmotions.Count)];
                ExerciseEmotion = randEmotion;
            }
            intEmotions.Add(randEmotion);
            allEmotions.Remove(randEmotion);
        }
        intEmotions = intEmotions.OrderBy(x => Random.value).ToList();
        foreach (var i in intEmotions)
        {
            var button = Instantiate(BtnEmotionPrefab, GridOfButtons.transform);
            button.SetEmotion(i);
            InstantiateButtons.Add(button.gameObject);
        }
        var contextStrings = gm.Emotions[ExerciseEmotion].Contexts;
        ExerciseText.text = contextStrings[Random.Range(0, contextStrings.Count)];
    }

    private void LoadImitateExercise()
    {
        if (ExerciseImage != null)
        {
            ExerciseEmotion = ExerciseImage.SetPhotoEmotionFromGMSelected();
        }
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
