using UnityEngine;

public class Yarnball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool goingRight;
    [SerializeField] private float rotationSpeed = 50f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            if (goingRight)
            {
                rb.AddForce(new Vector2(5f, 0f), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-5f, 0f), ForceMode2D.Impulse);
            }
        }
    }

    void Update()
    {
        if (goingRight)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
        }
    }
}
