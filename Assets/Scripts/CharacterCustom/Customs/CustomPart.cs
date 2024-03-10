using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CustomPart : MonoBehaviour
{
    protected Toggle toggle;
    public CharacterCustomization Customization { protected get; set; }

    protected virtual void Awake()
    {
        toggle = GetComponent<Toggle>();
        
        if (toggle != null) {
            toggle.onValueChanged.AddListener(delegate {
                SetPart();
            });
        }
    }
    
    protected virtual void Start() { }
    
    protected virtual void SetPart() { }
}