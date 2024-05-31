using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseActivity : EmotionExercise
{
    private static BaseActivity _instance;
    public static BaseActivity Instance {  get { return _instance; } }


    [SerializeField]
    protected EActivity Activity;
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField] 
    private int NumButtons = 4;
    [SerializeField]
    private int Score;

    [SerializeField]
    protected GameObject AreaOfButtons;
    [SerializeField]
    protected EmotionButton BtnEmotionPrefab;

    protected readonly int GoodScore = 10;
    protected readonly int BadScore = 5;

    protected List<GameObject> InstantiateButtons = new List<GameObject>();

    [SerializeField]
    protected AnswerPopUp PopUpGood;
    [SerializeField]
    protected AnswerPopUp PopUpBad;

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

        if (PopUpGood !=null && PopUpGood.PopUp != null)
            PopUpGood.PopUp.onClose += LoadExercise;
        if (PopUpBad !=null && PopUpBad.PopUp != null)
            PopUpBad.PopUp.onClose += LoadExercise;

    }

    private void CleanUp()
    {
        foreach (var go in InstantiateButtons)
            Destroy(go);
    }

    public void Good()
    {
        Score += GoodScore;
        UpdateScoreText();

        if (PopUpGood != null)
        {
            PopUpGood.PopUp.Open();
            PopUpGood.transform.SetAsLastSibling();
            PopUpGood.LoadAnswerCorrect(Exercises.Photos[CurrentExercise]); //ExerciseEmotion
            CleanUp();
        }
        else
        {
            LoadExercise();
        }
    }
    
    public void Bad(Emotion.EEmotion emotionSelected)
    {
        if (Score >= BadScore) Score -= BadScore;
        UpdateScoreText();

        if (PopUpBad != null)
        {
            PopUpBad.PopUp.Open();
            PopUpBad.transform.SetAsLastSibling();
            PopUpBad.LoadAnswerWrong(Exercises.Photos[CurrentExercise], emotionSelected);
            CleanUp();
        }
        else
        {
            LoadExercise();
        }
    }

    protected virtual void LoadExercise()
    {
        if (CurrentExercise == NumExcercises - 1)
        {
            StopCurrentExercise();
            OnLastExercise();
            return;
        }

        StopCurrentExercise();
        LoadCurrentExercise(CurrentExercise + 1);

        LoadEmotionButtons();
    }

    protected virtual void StopCurrentExercise() { }

    protected virtual void OnLastExercise()
    {
        UIActions.GoToGameComplete();
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

    protected virtual void LoadEmotionButtons()
    {
        CleanUp();

        var gm = GameManager.Instance;
        var selEmotions = new List<Emotion.EEmotion>(gm.SelectedEmotions); //AllEmotions allEmotions
        selEmotions.Remove(ExerciseEmotion);
        selEmotions = selEmotions.OrderBy(x => Random.value).ToList();

        if (selEmotions.Count >= NumButtons)
            selEmotions = selEmotions.GetRange(0, NumButtons - 1);

        selEmotions.Add(ExerciseEmotion);
        selEmotions = selEmotions.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < selEmotions.Count; i++)
        {
            var button = Instantiate(BtnEmotionPrefab, AreaOfButtons.transform);
            button.SetEmotion(selEmotions[i], i);
            InstantiateButtons.Add(button.gameObject);
        }
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Puntaje: " + Score;
    }
}
