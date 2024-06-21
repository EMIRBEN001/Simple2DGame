using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [SerializeField] private float movementDistance = 5f;
    [SerializeField] private float speed = 2f;


    private bool movingDown = true;
    private float downEdge;
    private float upEdge;

    private void Awake()
    {
        downEdge = transform.position.y - movementDistance;
        upEdge = transform.position.y + movementDistance;
    }

    private void Update()
    {
        if (movingDown)
        {
            if (transform.position.y > downEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingDown = false;
            }
        }
        else
        {
            if (transform.position.y < upEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingDown = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - movementDistance, transform.position.y, transform.position.z), new Vector3(transform.position.x + movementDistance, transform.position.y, transform.position.z));
    }
}
