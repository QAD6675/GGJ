using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatHUD : MonoBehaviour
{
    public Image catImage;               
    public TMP_Text livesText;           
    public Sprite[] catSprites;          
    public int maxLives = 9;       

    private int currentLives;

    void Start()
    {
        
        currentLives = maxLives;
        UpdateHUD();
    }

    
    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateHUD();
        }
    }

    public void GainLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateHUD();
        }
    }

   
    private void UpdateHUD()
    {
        
        catImage.sprite = catSprites[Mathf.Clamp(currentLives - 1, 0, catSprites.Length - 1)];

        
        livesText.text = currentLives.ToString();
    }
}
