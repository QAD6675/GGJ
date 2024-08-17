using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Death : MonoBehaviour
{
    
    [SerializeField] float RespawnTime;
    public Vector3 RespawnPosition;
    public GameObject Explosion;

    private void Start()
    {
        RespawnPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Harmful")
            Die();
    }

    public void Die()
    {   
        PlayExplosionEffect();
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), RespawnTime);
    }

    void Respawn()
    {
        gameObject.SetActive(true);

        transform.position = RespawnPosition;
    }

    void PlayExplosionEffect()
    {
        if (Explosion != null)
        {
            // Instantiate the explosion and play it
            GameObject explosionInstance = Instantiate(Explosion, transform.position, Quaternion.identity);
            ParticleSystem explosionParticles = explosionInstance.GetComponent<ParticleSystem>();
            
            if (explosionParticles != null)
            {
                explosionParticles.Play();
                // Destroy the explosion object after it finishes playing
                Destroy(explosionInstance, explosionParticles.main.duration);
            }
        }
    }
}
