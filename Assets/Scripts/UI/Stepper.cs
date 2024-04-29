using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(ToggleGroup))]
public class Stepper : MonoBehaviour
{
    // [SerializeField]
    public ToggleGroup Group { get; protected set; }
    List<Step> Steps = new List<Step>();
    [SerializeField]
    GameObject StepsGroup;
    [SerializeField][Tooltip("If isnt null will fill all steps from content childrens")]
    Transform ContentGroup;
    [SerializeField]
    Button ButtonPrev;
    [SerializeField]
    Button ButtonNext;
    [field:SerializeField]
    public int CurrentStep { get; protected set; }

    // To spawn
    [SerializeField]
    protected Step StepPrefab;

    // Delegates
    public delegate void OnStepChange();
    public OnStepChange onStepChange;
    public delegate void OnAllVisited();
    public OnAllVisited onAllVisited;

    // To snap if many steps
    [SerializeField]
    protected ScrollRect StepperScrollRect;
    [SerializeField]
    protected RectTransform StepperContent;

    // Visited
    // [SerializeField]
    protected uint NumVisited = 0;
    public bool AllVisited { get; protected set; }

    void Awake()
    {
        CurrentStep = -1;
        Group = gameObject.GetComponent<ToggleGroup>();
        AllVisited = false;
        LoadSteps();
        AddListeners();
    }


    void LoadSteps()
    {
        if (ContentGroup != null)
        {
            foreach (Transform child in ContentGroup)
            {
                AddStep(child.gameObject);
            }
        }
        else
        {
            if (StepsGroup != null)
                Steps = StepsGroup.GetComponentsInChildren<Step>().ToList<Step>();

            //gameObject.SetActive(false);
            if (Steps != null)
            {
                for (uint i = 0; i < Steps.Count; i++)
                {
                    Step step = Steps.ElementAt((int)i);
                    step.Fill(this, i);
                }
            }
        }

        SetDefaultStep(0);
    }

    /// LISTENERS
    void AddListeners()
    {
        if (ButtonPrev != null)
            ButtonPrev.onClick.AddListener(delegate { SetPreviousStep(); });

        if (ButtonNext != null)
            ButtonNext.onClick.AddListener(delegate { SetNextStep(); });

        //onStepChange += UpdateNavigationButtons;
    }
    
    void EnableButton(Button button, bool value)
    {
         //&& button.gameObject.activeSelf
        if (button != null)
        {
            button.interactable = value;
        }
    }
    void UpdateNavigationButtons() //uint index
    {
        if (Steps.Count < 2)
        {
            EnableButton(ButtonPrev, false);
            EnableButton(ButtonNext, false);
            return;
        }
        if (CurrentStep == 0)
        {
            EnableButton(ButtonPrev, false);
            EnableButton(ButtonNext, true);
        }
        else if (CurrentStep == Steps.Count - 1)
        {
            EnableButton(ButtonPrev, true);
            EnableButton(ButtonNext, false);
        }
        else
        {
            EnableButton(ButtonPrev, true);
            EnableButton(ButtonNext, true);
        }
    }

    //// ENABLING STEPS
    public void ChangeStep(int index)
    {
        if (index > Steps.Count || Steps.Count == 0) return;
        CurrentStep = index;
        UpdateNavigationButtons();

        // ChangeStep is called before updating WasVisited
        CheckIfVisited(Steps[index].WasVisited);

        RectTransform target = Steps[index].gameObject.GetComponent<RectTransform>();
        SnapTo(target);

        onStepChange?.Invoke();
        //Debug.Log("[Stepper] Current step: " + CurrentStep);
    }
    void EnableStep(bool value, int index)
    {
        if (index > Steps.Count || Steps.Count == 0) return;
        //Debug.Log("[Stepper]" + CurrentStep + (value ? " true" : " false"));
        if (Steps.ElementAt(index) == null) return;

        Steps[index].EnableStep(value);
    }
    void ChangeStepByNavigation(int index)
    {
        EnableStep(false, CurrentStep);
        EnableStep(true, index);
    }
    void SetPreviousStep()
    {
        if (CurrentStep == 0) { return; }
        ChangeStepByNavigation(CurrentStep - 1);
    }
    void SetNextStep()
    {
        if (CurrentStep == Steps.Count - 1) { return; }
        ChangeStepByNavigation(CurrentStep + 1);
    }

    void CheckIfVisited(bool wasVisited)
    {
        if (!AllVisited)
        {
            if (!wasVisited)
            {
                NumVisited++;
                //Debug.Log("[Stepper] NumVisited " + NumVisited);
            }

            if (NumVisited == Steps.Count)
            {
                AllVisited = true;
                onAllVisited?.Invoke();
            }
        }
    }

    /// AUTOGENERATION
    public void SetDefaultStep(int step = 0)
    {
        if (Steps.Count > 0)
        {
            EnableStep(true, step);
            //NumVisited = 1;
        }
    }
    public void AddStep(GameObject content)
    {
        if (StepPrefab == null || StepsGroup == null)
        {
            if (StepPrefab == null)
                Debug.Log("[Stepper] StepPrefab is null");
            if (StepsGroup == null)
                Debug.Log("[Stepper] StepsGroup is null");
            return;
        }

        Step step = Instantiate(StepPrefab, StepsGroup.transform);
        step.TabContent = content;
        step.Fill(this, (uint)Steps.Count);
        Steps.Add(step);
    }


    //// SNAPPING SCROLL
    public void SnapTo(RectTransform target)
    {
        if (target == null || StepperContent == null || StepperScrollRect == null) return;
        if (StepperContent.sizeDelta.x <= StepperScrollRect.preferredWidth) return;

        //https://stackoverflow.com/questions/30766020/how-to-scroll-to-a-specific-element-in-scrollrect-with-unity-ui
        Canvas.ForceUpdateCanvases();
        
        StepperContent.anchoredPosition =
                (Vector2)StepperScrollRect.transform.InverseTransformPoint(StepperContent.position)
                - (Vector2)StepperScrollRect.transform.InverseTransformPoint(target.position);
    }
}
