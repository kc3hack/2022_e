using UnityEngine;
/// <summary>
/// こんな感じで初期化処理だけ上書きして家電の種類を増やす
/// </summary>
public class ElectricSample : Electric
{
    protected override void Initialize()
    {
        this.hp = 2;
        this.moveSpeed = 50;
        this.name = "サンプル";
    }
}