using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int myHealth=100;
    [SerializeField] private int myDamage=100;
    [SerializeField] private int myResistance=0;
    [SerializeField] private bool Damagable=true; // make this false for spikes and stuff

    void Start(){}
    void Update(){}

    public HealthSystem myTarget;
    public bool hasTarget; //cuz it cannot null
    //when this object gets hit
    public void Hurt(int damage){
        if (Damagable)
        {
            myHealth-=(damage-myResistance);
        }
    }
    public int getHealth(){
        return myHealth;
    }
    // to hit someone else
    public void Hit(){
        if (!hasTarget) return;
        myTarget.Hurt(myDamage);
    }
}
