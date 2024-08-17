using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private float respawnTime;
    public Vector3 respawnPosition;
    public GameObject explosion;

    private void Start()
    {
        respawnPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Harmful"))
        {
            Die();
        }
    }

    public void Die()
    {   
        PlayExplosionEffect();
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), respawnTime);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = respawnPosition;
    }

    private void PlayExplosionEffect()
    {
        if (explosion != null)
        {
            // Instantiate the explosion and play it
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
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
