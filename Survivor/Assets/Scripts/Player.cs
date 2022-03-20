using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーキャラ
/// </summary>
public class Player : MonoBehaviour
{
    private float seconds; // 秒数カウント
    private float interval = 5; //発射間隔
    private float moveSpeed = 50; // 移動速度
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Generator.GetIsGame())
        {
            return;
        }
        seconds += Time.deltaTime; // 秒数カウント

        if (seconds > interval)
        {
            Attack();
            Debug.Log("発射");
            seconds = 0; //秒数カウント初期化
        }
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * this.moveSpeed;
    }

    /// <summary>
    /// 上下左右に攻撃する
    /// </summary>
    private void Attack()
    {
        Shot(0, 1, 90, 10); // 上方向

        Shot(1, 0, 0, 10); // 右方向
        Shot(-1, 0, 0, 10); // 左方向

        Shot(0, -1, 90, 10); // 下方向
    }

    /// <summary>
    /// 弾丸生成→発射 上方向
    /// </summary>
    public void Shot()
    {
        if (!Generator.GetIsGame())
        {
            return;
        }
        Bullet bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet")).GetComponent<Bullet>();
        bullet.gameObject.transform.parent = GameObject.FindGameObjectWithTag("PlayArea").transform;
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;
        bullet.Shot(x, y, 0, 1, 90, 10);
    }

    /// <summary>
    /// 弾丸生成→発射
    /// </summary>
    /// <param name="xDir">発射角度x</param>
    /// <param name="yDir">発射角度y</param>
    /// <param name="dir">弾丸回転</param>
    /// <param name="speed">弾丸速度</param>
    public void Shot(float xDir, float yDir, float dir, float speed)
    {
        if (!Generator.GetIsGame())
        {
            return;
        }
        Bullet bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet")).GetComponent<Bullet>();
        bullet.gameObject.transform.parent = GameObject.FindGameObjectWithTag("PlayArea").transform;
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;
        bullet.Shot(x, y, xDir, yDir, dir, speed);
    }




}
