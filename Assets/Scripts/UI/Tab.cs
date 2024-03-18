using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class Tab : MonoBehaviour
{
    public GameObject TabContent;
    protected Toggle TabButton;

    void Awake()
    {
        TabButton = gameObject.GetComponent<Toggle>();
        
        TabButton.onValueChanged.AddListener(delegate {
            UpdateActive(TabButton);
        });
    }

    void Start()
    {
        UpdateActive(TabButton);
    }

    protected virtual void UpdateActive(Toggle change)
    {
        if (TabButton == null || TabContent == null)
            return;
        
        TabContent.SetActive(change.isOn);
    }
}
