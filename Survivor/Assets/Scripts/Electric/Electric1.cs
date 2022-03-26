using UnityEngine;
/// <summary>
/// こんな感じで初期化処理だけ上書きして家電の種類を増やす
/// </summary>
public class Electric1 : Electric
{
    protected override void Initialize()
    {
        this.hp = 3;
        this.maxHp = 3;
        this.moveSpeed = 100;
        this.name = "扇風機";
    }
}