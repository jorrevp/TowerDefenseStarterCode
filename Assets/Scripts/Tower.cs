using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f; // Range within which the tower can detect and attack enemies 
    public float attackRate = 1f; // How often the tower attacks (attacks per second) 
    public int attackDamage = 1; // How much damage each attack does 
    public float attackSize = 1f; // How big the bullet looks 

    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
    public Enums.TowerType type; // the type of this tower

    public float projectileSpeed = 10f; // Snelheid van het projectiel

    private float nextAttackTime; // Tijd tot de volgende aanval 

    void Update()
    {
        // Controleer of het tijd is om aan te vallen
        if (Time.time >= nextAttackTime)
        {
            // Zoek naar vijanden binnen bereik
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // Schiet op de vijand
                    Shoot(collider.gameObject);
                    // Stel de volgende aanvalstijd in
                    nextAttackTime = Time.time + 1f / attackRate;
                    // Onderbreek de lus zodra een vijand is geraakt
                    break;
                }
            }
        }
    }
    void Shoot(GameObject target)
    {
        // Creëer een kogel en stel zijn eigenschappen in
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Projectiles projectile = bullet.GetComponent<Projectiles>();
        if (projectile != null)
        {
            projectile.damage = attackDamage;
            projectile.target = target.transform;
            bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f); // 1f toegevoegd om Z-schaal op 1 te houden (2D)

            Vector3 direction = (target.transform.position - transform.position).normalized;

            projectile.speed = projectileSpeed; // Assuming 'projectileSpeed' is a variable in Tower script
            bullet.GetComponent<Rigidbody2D>().velocity = direction * projectile.speed; // Assuming the bullet has a Rigidbody2D component
        }
        else
        {
            Debug.LogWarning("No Projectile component found on bulletPrefab!");
        }
    }
    // Teken het aanvalsgebied in de editor voor gemakkelijker debuggen 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
   
}
