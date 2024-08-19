using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{

    public TMP_Button playButton;
    public TMP_Button quitButton;
    public TMP_Button credsButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        credsButton.onClick.AddListener(OpenCreds);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void OpenCreds()
    {
        SceneManager.LoadScene("creds");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
