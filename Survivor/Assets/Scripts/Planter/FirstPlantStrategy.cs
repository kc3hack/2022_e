using UnityEngine;
/// <summary>
/// 第1段階での家具生成
/// 初期化時にplanterのインターバルを設定
/// Plantingが各段階で毎フレームごとに呼び出される処理になる
/// </summary>
public class FirstStratgey : PlanterStrategy
{
    private ElectricPlanter planter;

    public void Initialize()
    {

    }

    public void Planting()
    {
        if (planter.GetSeconds() > planter.GetInterval())
        {
            this.planter.SetTarget("Electric1");
            Debug.Log("つくる");
            planter.SetSeconds(0.0f);
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