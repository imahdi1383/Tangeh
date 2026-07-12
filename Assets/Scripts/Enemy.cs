using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public int health = 100;
    private float leftBoundary = -15f; // تنظیم بر اساس صحنه‌ات

    void Update()
    {
        // حرکت به چپ
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // اگر از مرز چپ رد شد، destroy کن
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
            // اینجا می‌تونی به GameManager بگی که بازنده شدی
            // GameManager.Instance.GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
