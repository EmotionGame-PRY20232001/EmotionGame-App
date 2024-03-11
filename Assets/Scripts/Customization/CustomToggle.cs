using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CustomToggle : MonoBehaviour
{
    protected Toggle toggle;
    
    protected virtual void Start()
    {
        SetOn();
        
        if (toggle != null) {
            toggle.onValueChanged.AddListener(delegate {
                ChangeSelection();
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

    protected virtual void ChangeSelection() { }
}