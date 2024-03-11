using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationLayout : MonoBehaviour
{
    [SerializeField]
    protected CharacterCustomization Customization;
    [SerializeField]
    protected List<CustomPart> CustomParts;
    
    void Awake()
    {
        if (Customization != null)
        {
            gameObject.GetComponentsInChildren<CustomPart>(true, CustomParts);
            foreach (CustomPart _part in CustomParts)
                _part.Customization = Customization;
        }
    }
    
    public void ApplySelection()
    {
        string _customization = Customization.GetJSON();
        Debug.Log("saving GuideJSON" + _customization);
        GameManager.Instance.SetCurrentPlayerGuide(_customization);
    }
}
