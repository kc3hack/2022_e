using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 発電所(仮)　インタフェースで戦略を決めるように拡張予定
/// これの生死＝ゲームの継続終了なので、大事な情報はここに集約させてる
/// → staticメソッドで確認可能
/// </summary>
public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject startpanel; // スタート画面
    [SerializeField] private GameObject resultpanel; // 結果画面
    private static Generator instance;
    private GeneratorStrategy strategy; // 戦略 
    private int hp = 3; // 耐久値
    private int interval = 3; // 発射間隔
    private float seconds; // 秒数カウント
    private float time; // 経過時間
    private bool isGame = false; // ゲームプレイ中かどうか
    private int phase = 0; // 敵の強さの段階
    private int wind = 0; // 風力 (0:なし,1:弱,2:並,3:強)
    private int fossil = 0; // 化石燃料資源量 (0:なし, 最大値とかを決めなきゃですね)
    private int weather = 0; // 天気 (0:曇り, 1:晴れ, 2:快晴)
    [SerializeField] private GameObject windText;
    [SerializeField] private GameObject fossilText;
    [SerializeField] private GameObject weatherText;

    /// <summary>
    /// 位置情報のゲッター
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetGeneratorPosition()
    {
        return instance.gameObject.transform.position;
    }

    /// <summary>
    /// ゲームプレイ中かどうか
    /// </summary>
    /// <returns></returns>
    public static bool GetIsGame()
    {
        return instance.isGame;
    }

    /// <summary>
    /// 敵の強さの段階のゲッター
    /// </summary>
    /// <returns></returns>
    public static int GetPhase()
    {
        return instance.phase;
    }

    /// <summary>
    /// ゲームの経過時間のゲッター
    /// </summary>
    /// <returns></returns>
    public static float GetTime()
    {
        return instance.time + 60.0f * instance.phase;
    }

    /// <summary>
    /// 風力のゲッター
    /// </summary>
    /// <returns></returns>
    public static int GetWind()
    {
        return instance.wind;
    }
    /// <summary>
    /// 天気のゲッター
    /// </summary>
    /// <returns></returns>
    public static int GetWeather()
    {
        return instance.weather;
    }
    /// <summary>
    /// 化石燃料量のゲッター
    /// </summary>
    /// <returns></returns>
    public static int GetFossil()
    {
        return instance.fossil;
    }


    /// <summary>
    /// HPのセッター
    /// </summary>
    /// <param name="hp"></param>
    public void SetHP(int hp)
    {
        this.hp = hp;
    }

    /// <summary>
    /// 射撃間隔のセッター
    /// </summary>
    /// <param name="interval"></param>
    public void SetInterval(int interval)
    {
        this.interval = interval;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (this.strategy == null)
        {
            Debug.Log("戦略はとりあえず適当にします");
            this.strategy = new SimpleStrategy();
        }
        strategy.SetGenerator(this);
        this.SetParam();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        time += Time.deltaTime;

        if (seconds > interval)
        {
            Attack();
            // Shot(); // 上向きに射撃 ここを変更すれば射撃を変更可能 → Strategyを実装したらAttack()に変更
            seconds = 0;
        }

        // このへんで、timeかsecondsみたいな感じで昼夜の設定とかをしても良いと思う

        if (time > 60.0f)
        {
            this.phase += 1; // フェイズの移行処理
            Debug.Log("第n段階に到達、敵が強化");
            this.time = 0;
            // 強化する
            // デザインパターンのObserverでElectricとかPlayerにPhaseの変更を通知するようにしても良いか
            // そうでなければ,それぞれのupdateでphaseの確認をさせるか
        }
    }

    /// <summary>
    /// 接触
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Electric": // 家電にあたったら被ダメして家電を消す
                this.Damaged();
                Destroy(collision.gameObject);
                break;
        }
    }

    /// <summary>
    /// 風力や天気などの環境情報を設定
    /// </summary>
    private void SetParam()
    {
        System.Random rand = new System.Random();
        this.wind = rand.Next() % 4;
        this.weather = rand.Next() % 3;
        this.fossil = rand.Next() % 100;

        string tex = "";

        tex = "風力:";
        if (wind == 0)
        {
            tex += "無し";
        }
        else if (wind == 1)
        {
            tex += "弱";
        }
        else if (wind == 2)
        {
            tex += "並";
        }
        else
        {
            tex += "強";
        }
        this.windText.GetComponent<Text>().text = tex;

        tex = "天気:";
        if (weather == 0)
        {
            tex += "曇り";
        }
        else if (weather == 1)
        {
            tex += "晴れ";
        }
        else
        {
            tex += "快晴";
        }
        this.weatherText.GetComponent<Text>().text = tex;

        tex = "化石:";
        tex += fossil;
        this.fossilText.GetComponent<Text>().text = tex;
    }

    /// <summary>
    /// 攻撃内容
    /// 戦略に委譲
    /// </summary>
    private void Attack()
    {
        this.strategy.Attack();
    }

    /// <summary>
    /// 弾丸生成→発射 上方向
    /// </summary>
    public void Shot()
    {
        if (!isGame)
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
        if (!isGame)
        {
            return;
        }
        Bullet bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet")).GetComponent<Bullet>();
        bullet.gameObject.transform.parent = GameObject.FindGameObjectWithTag("PlayArea").transform;
        float x = this.gameObject.transform.position.x;
        float y = this.gameObject.transform.position.y;
        bullet.Shot(x, y, xDir, yDir, dir, speed);
    }

    /// <summary>
    /// 被ダメ
    /// </summary>
    public void Damaged()
    {
        this.hp -= 1;
        Debug.Log("残りHP:" + hp);
        if (hp < 1)
        {
            this.GameSet();
        }
    }

    /// <summary>
    /// ゲーム開始の処理
    /// </summary>
    public void GameStart()
    {
        this.isGame = true;
        this.phase = 0;
        this.time = 0;
        this.startpanel.SetActive(false);
        this.strategy = GameManager.GetInstance().GetStrategy();
        this.strategy.SetGenerator(this);
        this.strategy.DefineHP();
        this.strategy.DefineInterval();
        Debug.Log("ゲームを始めるドン");
        // その他ゲーム開始時処理
    }

    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    public void GameSet()
    {
        Debug.Log("ゲーム終了だドン");
        this.isGame = false;
        this.resultpanel.SetActive(true);
        // スコア算出とか結果発表とかの終了後の処理
    }
}
