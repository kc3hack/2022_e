using UnityEngine;
/// <summary>
/// こんな感じで初期化処理だけ上書きして家電の種類を増やす
/// </summary>
public class Electric3 : Electric
{
    protected override void Initialize()
    {
        this.hp = 25;
        this.moveSpeed = 12;
        this.name = "冷蔵庫";
    }
}