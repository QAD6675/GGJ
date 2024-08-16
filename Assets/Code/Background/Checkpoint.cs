using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] SpriteRenderer _Sprite;
    [SerializeField] Sprite AvtivatedSprite;

    [SerializeField] AudioSource _AudioSource;
    bool playAudio = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Death>().RespawnPosition = transform.position;
            if (playAudio)
            {
                _AudioSource.Play();
                playAudio = false;
            }
            _Sprite.sprite = AvtivatedSprite;
        }
        //here
    }
}
