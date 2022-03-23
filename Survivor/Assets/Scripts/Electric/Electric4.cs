using UnityEngine;
/// <summary>
/// こんな感じで初期化処理だけ上書きして家電の種類を増やす
/// </summary>
public class Electric4 : Electric
{
    protected override void Initialize()
    {
        this.hp = 100;
        this.moveSpeed = 3;
        this.name = "エアコン";
    }
}