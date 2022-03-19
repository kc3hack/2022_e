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
    private string target = "Electric0"; // 生成対象 初期値:延長コード
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds > interval)
        {
            Debug.Log("つくる");
            seconds = 0.0f;
            Plant();
        }
    }

    /// <summary>
    /// 家電の生成
    /// </summary>
    private void Plant()
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
        System.Random r = new System.Random();
        float x = Generator.GetGeneratorPosition().x + 100 + (r.Next() % 10) * (r.Next() % 10);
        float y = Generator.GetGeneratorPosition().y + 100 + (r.Next() & 10) * (r.Next() % 10);
        Vector2 vector = new Vector2(x, y);
        return vector;
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
