using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;

    public Enums.Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        // Check how close we are to the target
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // If close, request a new waypoint
            target = EnemySpawner.Instance.RequestTarget(path, pathIndex);
            pathIndex++;

            // If target is null, we have reached the end of the path.
            // Destroy the enemy at this point
            if (target == null)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Function added to set the path index
    public void SetPathIndex(int index)
    {
        pathIndex = index;
    }
}
