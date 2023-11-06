using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject editButton;

    public void ActivateEdit()
    {
        if (editButton.activeSelf)
        {
            SceneManager.LoadScene("GameImitate");
        }
        editButton.SetActive(true);
    }
}
