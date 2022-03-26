using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// 第3段階での家具生成
/// 初期化時にplanterのインターバルを設定
/// Plantingが各段階で毎フレームごとに呼び出される処理になるので生成に関する条件などを書く
/// 生成位置はDefinePositionで決定
/// </summary>
public class FinalStratgey : PlanterStrategy
{
    private ElectricPlanter planter;
    private List<string> target = new List<string>();
    private int length = 4;
    private float[] generateSpeed = {0.3f, 0.3f, 0.3f, 0.3f};//SetSecondの引数
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
        float x = 250;
        float y = 250;
        float plantPositionX = Generator.GetGeneratorPosition().x;
        float plantPositionY = Generator.GetGeneratorPosition().y;
        for(;;){
            System.Random r = new System.Random();

            x = plantPositionX + (r.Next(-800, 800) + r.Next(-90, 90) + r.Next(-10, 10));
            y = plantPositionY + (r.Next(-800, 800) + r.Next(-90, 90) + r.Next(-10, 10));

            if(Math.Pow(x - plantPositionX, 2) + Math.Pow(y - plantPositionY, 2) > 1000000){
                continue;
            }
            if(Math.Pow(x - plantPositionX, 2) + Math.Pow(y - plantPositionY, 2) > 360000){
                Debug.Log("(" + x.ToString() + "," + y.ToString() + ")につくる");
                break;
            }
        }
        Vector2 vector = new Vector2(x, y);
        return vector;
    }

    public void SetPlanter(ElectricPlanter planter)
    {
        this.planter = planter;
    }
}