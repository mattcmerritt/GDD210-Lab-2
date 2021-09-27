using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody RB;
    public Transform Point1, Point2;
    public Vector3 Waypoint1, Waypoint2;
    public float MoveSpeed;
    public bool MovementDirection;
    public float Threshold;
    public bool ShowOutput;

    private void Start()
    {
        // saving the bounding waypoint locations
        Waypoint1 = Point1.position;
        Waypoint2 = Point2.position;
    }

    private void Update()
    {
        // moving towards point 1
        if (MovementDirection)
        {
            Vector3 direction = Vector3.Normalize(Waypoint1 - Waypoint2);
            transform.position += (direction * MoveSpeed * Time.deltaTime);

            // testing bounds
            if (ShowOutput)
            {
                Debug.Log("X: " + Mathf.Abs(Waypoint1.x - transform.position.x));
                Debug.Log("Z: " + Mathf.Abs(Waypoint1.z - transform.position.z));
            }

            // check if it has reached target
            if (Mathf.Abs(Waypoint1.x - transform.position.x) < Threshold && Mathf.Abs(Waypoint1.z - transform.position.z) < Threshold)
            {
                MovementDirection = !MovementDirection;
            }
        }
        // moving towards point 2
        else
        {
            Vector3 direction = -Vector3.Normalize(Waypoint1 - Waypoint2);
            transform.position += (direction * MoveSpeed * Time.deltaTime);

            // testing bounds
            if (ShowOutput)
            {
                Debug.Log("X: " + Mathf.Abs(Waypoint1.x - transform.position.x));
                Debug.Log("Z: " + Mathf.Abs(Waypoint1.z - transform.position.z));
            }

            // check if it has reached target
            if (Mathf.Abs(Waypoint2.x - transform.position.x) < Threshold && Mathf.Abs(Waypoint2.z - transform.position.z) < Threshold)
            {
                MovementDirection = !MovementDirection;
            }
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.TakeDamage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UI ui = FindObjectOfType<UI>();
            if (ui != null)
            {
                ui.DisableHitIndicator();
            }
        }
    }
}
