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
        
        TabContent.SetActive(TabButton.isOn);
    }
}
