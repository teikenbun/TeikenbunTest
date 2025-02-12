using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        // 确保碰撞的是子弹
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);  // 销毁子弹
            Destroy(gameObject);            // 销毁敌人
        }
    }
}