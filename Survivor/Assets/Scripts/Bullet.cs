using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾丸の基本クラス
/// 弾丸の種類を増やすときはこれを拡張する
/// </summary>
public class Bullet : MonoBehaviour
{
    protected int chargePoint; // 夜ダメージ
    private Rigidbody2D rigidBody;
    private bool isSet = false; // 発射可能
    private float xDir = 0; // 発射角度初期値0
    private float yDir = 0; // 発射角度初期値0
    protected float speed = 0; // 移動速度初期値0

    void Start()
    {
        this.rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSet)
        {
            this.rigidBody.AddForce(new Vector2(this.xDir, this.yDir) * speed, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// エネルギー弾を発射
    /// </summary>
    /// <param name="x">発射位置x</param>
    /// <param name="y">発射位置y</param>
    /// <param name="xDir">発射角度x</param>
    /// <param name="yDir">発射角度y</param>
    /// <param name="dir">弾丸の回転</param>
    /// <param name="spd">発射速度</param>
    public virtual void Shot(float x, float y, float xDir, float yDir, float dir, float spd)
    {
        this.gameObject.transform.Rotate(0, 0, dir);
        this.gameObject.transform.position = new Vector3(x, y, 0);
        this.xDir = xDir;
        this.yDir = yDir;
        this.speed = spd;
        this.isSet = true;
    }

    /// <summary>
    /// 判定持ちと接触時の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Electric":
                Debug.Log("家電にあたった");
                OnHitElectric(collision);
                Destroy(this.gameObject);
                break;
            case "End":
                Destroy(this.gameObject);
                break;
        }
    }

    /// <summary>
    /// 家電に衝突時の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnHitElectric(Collider2D collision)
    {
        Debug.Log("家電に充電");
        // あたった家電の被弾メソッドを引き出す
        collision.GetComponent<Electric>().Charge(this.chargePoint);
    }
}
