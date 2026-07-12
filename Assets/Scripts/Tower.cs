using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1.5f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
