using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleCustom : MonoBehaviour
{
    protected Toggle toggle;
    
    protected virtual void Start()
    {
        SetOn();
        
        if (toggle != null) {
            toggle.onValueChanged.AddListener(delegate {
                ChangeIfSelected(toggle);
            });
        }
    }

    public virtual void SetOn()
    {
        toggle = GetComponent<Toggle>();
        Player player = GameManager.Instance.GetCurrentPlayer();
        toggle.isOn = IsSameCustomActive(player);
    }
    
    protected virtual bool IsSameCustomActive(Player player) { return false; }

    protected virtual void ChangeIfSelected(Toggle _toggle)
    {
        if (_toggle.isOn)
            ChangeSelection();
    }

    protected virtual void ChangeSelection() { }
}