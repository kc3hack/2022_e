using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミー(家電)の基本形
/// これを拡張して他の家電は作る
/// </summary>
public class Electric : MonoBehaviour
{
    protected int maxHp; // 最大体力値
    protected int hp; // 体力
    protected float moveSpeed; // 移動速度
    protected string ename; // 家具名
    private Rigidbody2D rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        this.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public string GetName()
    {
        return this.ename;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected virtual void Initialize()
    {

    }

    /// <summary>
    /// 移動
    /// </summary>
    public void Move()
    {
        float xDir = transform.position.x - Generator.GetGeneratorPosition().x;
        float yDir = transform.position.y - Generator.GetGeneratorPosition().y;
        xDir = xDir < 0 ? 1.0f : -1.0f;
        yDir = yDir < 0 ? 1.0f : -1.0f;

        this.rigidBody.velocity = new Vector2(xDir, yDir) * moveSpeed;
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">被ダメージ</param>
    public void Charge(int damage)
    {
        Debug.Log(this.ename + "はじゅうでんされた");
        this.hp -= damage;
        if (this.hp < 1)
        {
            Generator.AddScore(this.maxHp);
            System.Random rand = new System.Random();
            if (rand.Next() % 10 < 1)
            {
                Generator.GetPlayer().Enforce();
            }
            Destroy(this.gameObject);
        }
    }
}
