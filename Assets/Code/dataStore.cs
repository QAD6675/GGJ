using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataStore : MonoBehaviour
{
    public int lives = 9;

    private static dataStore instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
