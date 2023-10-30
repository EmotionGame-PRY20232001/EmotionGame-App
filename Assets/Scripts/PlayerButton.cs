using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject editButton;

    public void ActivateEdit()
    {
        editButton.SetActive(!editButton.activeSelf);
    }
}
