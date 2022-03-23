using UnityEngine;
/// <summary>
/// 第3段階での家具生成
/// 初期化時にplanterのインターバルを設定
/// Plantingが各段階で毎フレームごとに呼び出される処理になるので生成に関する条件などを書く
/// 生成位置はDefinePositionで決定
/// </summary>
public class FifthStratgey : PlanterStrategy
{
    private ElectricPlanter planter;

    public void Initialize()
    {

    }

    public void Planting()
    {
        this.planter.SetTarget("Electric1");
        Debug.Log("つくる");
        planter.SetSeconds(5.0f);
        planter.Plant();
    }

    public Vector2 DefinePosition()
    {
        Vector2 ret = new Vector2(0, 0);
        // ここで生成位置決定
        return ret;
    }

    public void SetPlanter(ElectricPlanter planter)
    {
        this.planter = planter;
    }
}