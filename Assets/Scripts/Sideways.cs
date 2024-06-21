using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance = 5f;
    [SerializeField] private float speed = 2f;

    private bool movingLeft = true;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - movementDistance, transform.position.y, transform.position.z), new Vector3(transform.position.x + movementDistance, transform.position.y, transform.position.z));
    }
}
