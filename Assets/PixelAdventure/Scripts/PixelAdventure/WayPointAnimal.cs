using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointAnimal : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 4f;

    private SpriteRenderer sprite;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

        float huong_x = transform.position.x;
        if(huong_x < waypoints[currentWaypointIndex].transform.position.x)
        {
            sprite.flipX = true;
        }
        if(huong_x > waypoints[currentWaypointIndex].transform.position.x)
        {
            sprite.flipX = false;
        }
    }
}
