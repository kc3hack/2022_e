using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーキャラ
/// </summary>
public class Player : MonoBehaviour
{
    private float seconds; // 秒数カウント
    private float interval = 5; //発射間隔
    private float moveSpeed = 50; // 移動速度
    private int point = 5; // 与ダメージ
    private int shotType = 1; // 射撃方法
    private float charge = 0.5f;
    private Vector2 direction = new Vector2(0, 0); // プレイヤーの向き
    [SerializeField] Text text;
    [SerializeField] Text chargeText;

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

        if (Input.GetKey(KeyCode.Space))
        {
            if (this.charge < 10)
            {
                this.charge += Time.deltaTime;
                this.point = (int)this.charge * 5;
                int ch = (int)(this.charge * 5 * 72);
                this.chargeText.text = "電力量 " + ch + " kJ";
                //SEManager.ChargeP();
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (point < 1)
            {
                return;
            }
            Attack();
            this.chargeText.text = "電力量 0 kJ";
            this.charge = 0.5f;
        }

        if (seconds > interval)
        {
            //Attack();
            Debug.Log("発射");
            seconds = 0; //秒数カウント初期化
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * this.moveSpeed;

        if (x != 0 || y != 0)
        {
            this.direction = new Vector2(x, y).normalized;
        }
    }

    /// <summary>
    /// 強化処理
    /// </summary>
    public void Enforce()
    {
        System.Random rand = new System.Random();
        int r = rand.Next();
        if (r % 3 == 0)
        {
            // 移動速度UP
            if (this.moveSpeed < 70)
            {
                this.moveSpeed = 70;
            }
            else
            {
                this.moveSpeed = 100;
            }
            Debug.Log("はやくなった");
        }
        else if (r % 3 == 1)
        {
            // 射撃方法UP
            if (this.shotType == 1)
            {
                this.shotType = 2;
            }
            else
            {
                this.shotType = 3;
            }
            Debug.Log("うちかたふえた");
        }
        else if (r % 3 == 2)
        {
            // 射撃間隔UP
            if (this.interval == 5)
            {
                this.interval = 4;
            }
            else
            {
                this.interval = 3;
            }
            Debug.Log("ひんどあがった");
        }

        string param = "移動";
        if (this.moveSpeed == 70)
        {
            param += "+";
        }
        else if (this.moveSpeed == 100)
        {
            param += "++";
        }
        param += " 射撃";
        if (this.shotType == 2)
        {
            param += "+";
        }
        else if (this.shotType == 3)
        {
            param += "++";
        }
        param += " 間隔";
        if (this.interval == 4)
        {
            param += "+";
        }
        else if (this.interval == 3)
        {
            param += "++";
        }
        this.text.text = param;
    }

    /// <summary>
    /// プレイヤーの弱体化
    /// </summary>
    public void DeForce()
    {
        this.moveSpeed = 50;
    }

    /// <summary>
    /// 段階に応じて攻撃する
    /// </summary>
    private void Attack()
    {
        if (this.shotType == 1)
        {
            this.Shot01();
        }
        else if (this.shotType == 2)
        {
            this.Shot02();
        }
        else if (this.shotType == 3)
        {
            this.Shot03();
        }
        SEManager.ShotP();
    }

    private float CalculateAngle()
    {
        float angle = 0f;
        float x = this.direction.x;
        float y = this.direction.y;
        angle = Mathf.Acos(x / Mathf.Sqrt(x * x + y * y));
        angle = angle * 180.0f / Mathf.PI;
        if (y < 0)
        {
            angle = 360.0f - angle;
        }
        return angle;
    }

    /// <summary>
    /// 射撃第1段階
    /// </summary>
    private void Shot01()
    {
        float rot = CalculateAngle();
        this.Shot(this.direction.x, this.direction.y, rot, 10);
    }

    /// <summary>
    /// 射撃第2段階
    /// </summary>
    private void Shot02()
    {
        float rot = CalculateAngle();
        this.Shot(this.direction.x, this.direction.y, rot, 15);

        this.Shot(-this.direction.x, -this.direction.y, rot + 180, 10);
    }

    /// <summary>
    /// 射撃第3段階
    /// </summary>
    public void Shot03()
    {
        float rot = CalculateAngle();
        this.Shot(this.direction.x, this.direction.y, rot, 15);
        this.Shot(Mathf.Cos((CalculateAngle() + 30f) * Mathf.PI / 180), Mathf.Sin((CalculateAngle() + 30f) * Mathf.PI / 180), rot + 30, 15);
        this.Shot(Mathf.Cos((CalculateAngle() - 30f) * Mathf.PI / 180), Mathf.Sin((CalculateAngle() - 30f) * Mathf.PI / 180), rot - 30, 15);

        this.Shot(-this.direction.x, -this.direction.y, rot + 180, 15);
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
        //SEManager.ShotP();
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
        bullet.SetChargePoint(this.point);
        //SEManager.ShotP();
        bullet.Shot(x, y, xDir, yDir, dir, speed);
    }




}
