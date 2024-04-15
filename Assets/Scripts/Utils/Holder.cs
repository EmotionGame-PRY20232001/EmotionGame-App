using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//https://stackoverflow.com/questions/62361746/how-to-take-button-hold-as-an-input-in-unity-c-sharp
//https://stackoverflow.com/questions/55448551/unity3d-how-to-detect-when-a-button-is-being-held-down-and-released
public class Holder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public EHoldState HoldState { get; protected set; }
    [SerializeField]
    protected float HoldTime = 0.1f;
    public bool interactable;

    /// Delegates
    public delegate void OnHold();
    public OnHold onHold;
    public delegate void OnRelease();
    public OnRelease onRelease;
    public delegate void OnHolding();
    public OnHolding onHolding;

    [SerializeField]
    protected Button Btn;

    void Awake()
    {
        HoldState = EHoldState.Inactive;
    }

    private void Start()
    {
        SetUpButon();
    }

    IEnumerator OnHoldEnumerator()
    {
        yield return new WaitForSeconds(HoldTime);

        onHold?.Invoke();
        HoldState = EHoldState.Holding;

        if (onHolding != null)
            StartCoroutine(OnHoldingEnumerator());
    }

    IEnumerator OnHoldingEnumerator()
    {
        while (true)
        {
            onHolding.Invoke();
            yield return null;
        }
    }

    protected void OnReleaseHold()
    {
        StopAllCoroutines();

        if (HoldState == EHoldState.Holding)
            onRelease?.Invoke();

        HoldState = EHoldState.Inactive;
    }

    protected void SetUpButon()
    {
        Btn = gameObject.GetComponent<Button>();
        if (Btn != null)
            interactable = Btn.interactable;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;

        if (HoldState == EHoldState.Inactive)
            HoldState = EHoldState.Awaiting;
        StartCoroutine(OnHoldEnumerator());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnReleaseHold();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnReleaseHold();
    }

    public void OnPointerEnter(PointerEventData eventData) { }

    public enum EHoldState
    {
        Inactive,
        Awaiting,
        Holding,
    }
}