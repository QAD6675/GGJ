using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CatHUD : MonoBehaviour
{
    public Image catImage;            
    public TMP_Text livesText;           
    public Sprite[] catSprites;          
    public int maxLives = 9;      

    void Start()
    {
        UpdateHUD();
    }

    
    public void LoseLife()
    {
        if (dataStore.instance.lives == 1){
            dataStore.instance.lives=maxLives;
            SceneManager.LoadScene("level 1");
            return;
        }
        if (dataStore.instance.lives> 0)
        {
            dataStore.instance.lives--;
            UpdateHUD();
        }
    }

    public void GainLife()
    {
        if (dataStore.instance.lives < maxLives)
        {
            dataStore.instance.lives++;
            UpdateHUD();
        }
    }

   
    private void UpdateHUD()
    {
        
        catImage.sprite = catSprites[dataStore.instance.lives-1];

        
        livesText.text = dataStore.instance.lives.ToString();
    }
}
