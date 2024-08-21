using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{

    public Button playButton;
    public Button credsButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
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
}
