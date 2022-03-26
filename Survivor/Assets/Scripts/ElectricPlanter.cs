using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 家電生成器
/// </summary>
public class ElectricPlanter : MonoBehaviour
{
    private float seconds = 0.0f; //経過時間
    private float interval = 10.0f; // 家電生成間隔
    [SerializeField] private string target = "Electric0"; // 生成対象 初期値:延長コード
    private PlanterStrategy first;
    private PlanterStrategy second;
    private PlanterStrategy third;
    private PlanterStrategy fourth;
    private PlanterStrategy fifth;
    private PlanterStrategy final;

    /// <summary>
    /// 経過時間の取得
    /// </summary>
    /// <returns></returns>
    public float GetSeconds()
    {
        return this.seconds;
    }

    /// <summary>
    /// 家電生成間隔の取得
    /// </summary>
    /// <returns></returns>
    public float GetInterval()
    {
        return this.interval;
    }

    /// <summary>
    /// 経過時間のセッター
    /// /// </summary>
    public void SetSeconds(float seconds)
    {
        this.seconds = seconds;
    }

    /// <summary>
    /// 家具生成間隔のセッター
    /// </summary>
    /// <param name="interval"></param>
    public void SetInterval(float interval)
    {
        this.interval = interval;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Generator.GetIsGame())
        {
            return;
        }

        seconds += Time.deltaTime;
        if (Generator.GetPhase() == 0)
        {
            first.Planting();
        }
        else if (Generator.GetPhase() == 1)
        {
            second.Planting();
        }
        else if (Generator.GetPhase() == 2)
        {
            third.Planting();
        }
        else if (Generator.GetPhase() == 3)
        {
            fourth.Planting();
        }
        else if (Generator.GetPhase() == 4)
        {
            fifth.Planting();
        }
        else
        {
            final.Planting();
        }
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        /// 各戦略の生成、セット、初期化
        this.first = new FirstStratgey();
        first.SetPlanter(this);
        first.Initialize();

        this.second = new SecondStratgey();
        second.SetPlanter(this);
        second.Initialize();

        this.third = new ThirdStratgey();
        third.SetPlanter(this);
        third.Initialize();

        this.fourth = new FourthStratgey();
        fourth.SetPlanter(this);
        fourth.Initialize();

        this.fifth = new FifthStratgey();
        fifth.SetPlanter(this);
        fifth.Initialize();

        this.final = new FinalStratgey();
        final.SetPlanter(this);
        final.Initialize();
    }

    /// <summary>
    /// 家電の生成
    /// </summary>
    public void Plant()
    {
        if (!Generator.GetIsGame()) // ゲームをプレイ中かの確認
        {
            return;
        }
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + this.target);
        if (prefab == null) // 生成対象の有無の確認
        {
            Debug.Log("そんな家電はないよ");
            return;
        }
        GameObject obj = Instantiate(prefab); // 家電の生成、場所や親子関係の変更
        obj.transform.parent = GameObject.FindGameObjectWithTag("PlayArea").transform;
        obj.transform.position = DefinePosition();
        Debug.Log("作った");
    }

    /// <summary>
    /// 家電の生成位置の決定
    /// 要・調整
    /// </summary>
    /// <returns></returns>
    private Vector2 DefinePosition()
    {
        Vector2 ret = new Vector2(0, 0);
        if (Generator.GetPhase() == 0)
        {
            ret = first.DefinePosition();
        }
        else if (Generator.GetPhase() == 1)
        {
            ret = second.DefinePosition();
        }
        else if (Generator.GetPhase() == 2)
        {
            ret = third.DefinePosition();
        }
        else if (Generator.GetPhase() == 3)
        {
            ret = fourth.DefinePosition();
        }
        else if (Generator.GetPhase() == 4)
        {
            ret = fifth.DefinePosition();
        }
        else
        {
            ret = final.DefinePosition();
        }
        return ret;
    }

    /// <summary>
    /// 生成対象の変更
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(string target)
    {
        this.target = target;
    }
}
