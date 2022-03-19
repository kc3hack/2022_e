using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected int hp = 3; // 耐久値
    protected int interval = 3; // 発射間隔
    private float seconds; // 秒数カウント
    private float time; // 経過時間
    private bool isGame = false; // ゲームプレイ中かどうか
    private int phase = 0; // 敵の強さの段階

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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        time += Time.deltaTime;

        if (seconds > interval)
        {
            Shot();
            seconds = 0;
        }

        // このへんで、timeかsecondsみたいな感じで昼夜の設定とかをしても良いと思う

        if (time > 60.0f)
        {
            this.phase += 1; // フェイズの移行処理
            Debug.Log("第n段階に到達、敵が強化");
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
    public static void GameStart()
    {
        instance.isGame = true;
        instance.phase = 1;
        instance.time = 0;
        instance.startpanel.SetActive(false);
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
