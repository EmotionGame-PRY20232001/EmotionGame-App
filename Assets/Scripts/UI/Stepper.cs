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
    [SerializeField]
    Button ButtonPrev;
    [SerializeField]
    Button ButtonNext;
    [field:SerializeField]
    public int CurrentStep { get; protected set; }

    // To spawn
    [SerializeField]
    protected Step StepPrefab;
    public delegate void OnStepChange();
    public OnStepChange onStepChange;

    void Awake()
    {
        Group = gameObject.GetComponent<ToggleGroup>();
        LoadSteps();
        AddListeners();
    }

    void FillStep(Step step, uint index)
    {
        step.Stepper = this;
        step.Index = index;

        if (Group != null)
            step.TabButton.group = Group;
    }

    void LoadSteps()
    {
        // StepsGroup
        if (StepsGroup != null)
            Steps = StepsGroup.GetComponentsInChildren<Step>().ToList<Step>();

        //gameObject.SetActive(false);
        if (Steps != null)
        {
            for (uint i = 0; i < Steps.Count; i++)
            {
                Step step = Steps.ElementAt((int)i);
                FillStep(step, i);
            }
            ChangeStep(0);
        }
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
        onStepChange?.Invoke();
        Debug.Log("[Stepper] Current step: " + CurrentStep);
    }
    void EnableStep(bool value, int index)
    {
        if (index > Steps.Count || Steps.Count == 0) return;
        // Debug.Log(CurrentStep + (value ? " true" : " false"));
        if (Steps.ElementAt(index) != null)
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

    //// AUTOGENERATION
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
        FillStep(step, (uint)Steps.Count);

        Steps.Add(step);
        EnableStep(true, CurrentStep);
    }
}
