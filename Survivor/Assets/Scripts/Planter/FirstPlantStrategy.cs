using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 第1段階での家具生成
/// 初期化時にplanterのインターバルを設定
/// Plantingが各段階で毎フレームごとに呼び出される処理になる
/// </summary>
public class FirstStratgey : PlanterStrategy
{
    private ElectricPlanter planter;
    private List<string> target = new List<string>();
    private int length = 2;
    private float[] generateSpeed = {0.5f, 0.5f};//SetSecondの引数
    public void Initialize()
    {
        //家電を配列に入れる
        for(int i = 0; i < length; i++){
            target.Add("Electric" + (i+1).ToString());
        }
    }

    public void Planting()
    {
        if (planter.GetSeconds() <= planter.GetInterval())
        {
            return;
        }
        
        for(int i = 0; i < length; i++){
            this.planter.SetTarget(target[i]);
            Debug.Log("つくる");
            planter.SetSeconds(generateSpeed[i]);
            planter.Plant();
        }
    }
    /// <summary>
    /// 家電の生成位置の決定
    /// 要・調整　第1象限にだけ出るようにしてる
    /// </summary>
    /// <returns></returns>
    public Vector2 DefinePosition()
    {
        System.Random r = new System.Random();
        float x = Generator.GetGeneratorPosition().x + 250 + (r.Next() % 100) * (r.Next() % 10);
        float y = Generator.GetGeneratorPosition().y + 250 + (r.Next() & 10) * (r.Next() % 10);
        Vector2 vector = new Vector2(x, y);
        return vector;
    }

    public void SetPlanter(ElectricPlanter planter)
    {
        this.planter = planter;
    }
}