using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] SpriteRenderer _Sprite;
    [SerializeField] Sprite ActivatedSprite;

    [SerializeField] AudioSource _AudioSource;
    bool playAudio = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Death>().respawnPosition = transform.position;
            if (playAudio)
            {
                _AudioSource.Play();
                playAudio = false;
            }
            _Sprite.sprite = ActivatedSprite;
        }
        //here
    }
}
