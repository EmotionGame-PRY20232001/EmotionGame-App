using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class Tab : MonoBehaviour
{
    public GameObject TabContent;
    public Toggle TabButton { get; protected set; }
    protected bool wasOn = false;

    protected virtual void Awake()
    {
        TabButton = gameObject.GetComponent<Toggle>();
        //wasOn = !TabButton.isOn;
        //Debug.Log(name + " Awake \twason " + wasOn + "\t isON " + TabButton.isOn);
    }

    protected virtual void Start()
    {
        UpdateActive(TabButton);

        TabButton.onValueChanged.AddListener(delegate {
            UpdateActive(TabButton);
        });
    }

    protected virtual void UpdateActive(Toggle change)
    {
        if (TabButton == null || TabContent == null ||
            wasOn == TabButton.isOn)
            return;

        //Debug.Log(name + " UpdateActive \twasOn " + wasOn + "\t isON " + TabButton.isOn);

        TabContent.SetActive(TabButton.isOn);
        wasOn = TabButton.isOn;
    }
}
