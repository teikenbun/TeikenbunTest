using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public GameObject bulletPrefab;  // 在 Unity Inspector 里赋值
    public Transform firePoint;      // 子弹发射位置
    public Transform spriteTransform;
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal"); // A/D 或 左/右
        float moveY = Input.GetAxisRaw("Vertical");   // W/S 或 上/下

        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);


        RotateSpriteToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void RotateSpriteToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, spriteTransform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 确保碰撞的是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Destroy(gameObject);            // 销毁子弹
            gameOverPanel.SetActive(true);
        }
    }
}
