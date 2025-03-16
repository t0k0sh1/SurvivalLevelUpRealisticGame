using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShootProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float shootInterval = 1f;
    [SerializeField] int projectileCount = 3;
    [SerializeField] float spreadAngle = 15f;
    [SerializeField] int projectileDamage = 50;

    float shootTimer;
    private void Start()
    {
        shootTimer = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Shoot();
        // }

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 shootDir = (mousePos - transform.position).normalized;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = spreadAngle * (i - (projectileCount - 1) / 2);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * shootDir;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);

            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

            projectile.GetComponent<Projectile>().damage = projectileDamage;
        }

        // GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // projectile.GetComponent<Rigidbody2D>().velocity = shootDir * projectileSpeed;
    }
}
