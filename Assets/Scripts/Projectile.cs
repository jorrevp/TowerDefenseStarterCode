using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;
    
    void Start()
    {
        RotateTowardsTarget();
    }
    void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            target.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);
        } 
    }
    
}
