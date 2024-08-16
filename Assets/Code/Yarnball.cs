using System.Collections;
using UnityEngine;

public class Yarnball : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private bool goingRight;
    [SerializeField]private float rotationSpeed=50f;
    
    public Transform transform;

    // Start is called before the first frame update
    void Start(){

    }
    // Update is called once per frame
    void Update()
    {
        if(goingRight){
            transform.Rotate(Vector3.forward*Time.deltaTime*rotationSpeed);
        }else{
            transform.Rotate(-Vector3.forward*Time.deltaTime*rotationSpeed);
        }
    }
}
