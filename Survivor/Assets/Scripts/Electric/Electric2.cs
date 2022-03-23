using UnityEngine;
/// <summary>
/// こんな感じで初期化処理だけ上書きして家電の種類を増やす
/// </summary>
public class Electric2 : Electric
{
    protected override void Initialize()
    {
        this.hp = 15;
        this.moveSpeed = 20;
        this.name = "パソコン";
    }
}