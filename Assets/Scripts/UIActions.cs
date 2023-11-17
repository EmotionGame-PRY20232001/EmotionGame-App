using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIActions : MonoBehaviour
{
    public void ReturnToSelecion()
    {
        SceneManager.LoadScene("SelectPlayer");
    }

    public void Play()
    {
        SceneManager.LoadScene("GameImitate");
    }
}
