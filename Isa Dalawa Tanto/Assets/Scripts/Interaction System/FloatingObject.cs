using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float topBoundary = 0.75f;
    [SerializeField] private float bottomBoundary = -0.75f;
    [SerializeField] private int direction = 1; //1 is up; -1 is down; 0 is stationary
    [SerializeField] private float speed = 2f;

    private Vector3 topBoundaryPosition;
    private Vector3 bottomBoundaryPosition;

    private void Start()
    {
        topBoundaryPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + topBoundary, transform.localPosition.z);
        topBoundaryPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + bottomBoundary, transform.localPosition.z);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        switch (direction)
        {
            case 1:
                transform.localPosition += Vector3.up * Time.deltaTime * speed;
                if (transform.localPosition.y >= topBoundary) direction = -1;
                break;

            case -1:
                transform.localPosition += Vector3.down * Time.deltaTime * speed;
                if (transform.localPosition.y <= bottomBoundary) direction = 1;
                break;

            default: direction = 0; break;
        }
    }
}
