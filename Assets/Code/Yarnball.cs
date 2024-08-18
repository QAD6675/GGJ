using UnityEngine;

public class Yarnball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool goingRight;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private SpriteRenderer sr;
    private bool rolling=false;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            if(rolling)return;
            if (goingRight)
            {
                rb.AddForce(new Vector2(7f, 0f), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-7f, 0f), ForceMode2D.Impulse);
            }
        }
    }
    public void caughtCat(){
        sr.enabled=false;
    }
    public void releasetCat(){
        enabled=false;
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
