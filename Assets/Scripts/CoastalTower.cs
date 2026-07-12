using UnityEngine;

public class CoastalTower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // نقطه‌ای که گلوله ازش شلیک میشه
    public float fireRate = 1f; // هر چند ثانیه یک بار شلیک
    public float range = 10f; // برد شلیک

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Shoot(nearestEnemy);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = range;

        foreach (GameObject enemy in enemies)
        {
            // فقط دشمن‌های سمت راست
            if (enemy.transform.position.x <= transform.position.x)
                continue;

            // چک کردن اینکه در همون ردیف افقی هست (با تلرانس)
            float verticalDiff = Mathf.Abs(enemy.transform.position.y - transform.position.y);
            if (verticalDiff > 0.5f) // تلرانس برای ارتفاع
                continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }


    void Shoot(GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.target = target.transform;
        }
    }
}
