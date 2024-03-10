using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationOptions : MonoBehaviour
{
    [SerializeField]
    protected CharacterCustomization Customization;
    [SerializeField]
    protected CustomPart[] CustomParts;
    
    void Awake()
    {
        if (Customization != null)
        {
            CustomParts = gameObject.GetComponentsInChildren<CustomPart>();
            foreach (CustomPart _part in CustomParts)
                _part.Customization = Customization;
        }
    }
}
