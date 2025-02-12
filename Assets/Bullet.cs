using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // 按照自身朝向进行移动
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {

        // 当子弹离开屏幕时销毁
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 确保碰撞的是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Access access = FindFirstObjectByType<Access>();
            access.score();
            Destroy(collision.gameObject);  // 销毁敌人
            Destroy(gameObject);            // 销毁子弹
            
        }
    }
}
