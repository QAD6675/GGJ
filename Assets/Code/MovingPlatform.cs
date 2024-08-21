using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] MovePoints;
    Vector3[] points;
    int currentPoint = 1;

    [Space]

    [SerializeField] float Speed;

    [Space]

    bool reverse = false;
    bool move = false;

    private void Start()
    {
        if (MovePoints.Length > 0)
        {
            points = new Vector3[MovePoints.Length];
            for (int i = 0; i < MovePoints.Length; i++)
            {
                points[i] = MovePoints[i].position;
            }
        }
    }

    private void Update()
    {
        if (!move)
            return;

        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint], Speed * Time.deltaTime);

        if (transform.position == points[currentPoint])
        {
            if (currentPoint == points.Length - 1)
                reverse = true;
            else if (currentPoint == 0)
                reverse = false;

            currentPoint += reverse ? -1 : 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming "Player" is the tag you're interested in
        {
            collision.transform.SetParent(transform, true);
            move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.parent = null;
    }
}

