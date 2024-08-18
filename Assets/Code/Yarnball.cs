using UnityEngine;

public class Yarnball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool goingRight;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private SpriteRenderer sr;
    private bool rolling=false;
    private bool hasCat = false;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            if(!hasCat)return;
            if(rolling)return;
            if (goingRight)
            {
                rb.AddForce(new Vector2(3f, 0f), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-3f, 0f), ForceMode2D.Impulse);
            }
        }
    }
    public void caughtCat(){
        hasCat=true;
        sr.enabled=false;
    }
    public void releasetCat(){
        hasCat=false;
        Destroy(this);
    }

    void Update()
    {
        if (!hasCat)return;
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
