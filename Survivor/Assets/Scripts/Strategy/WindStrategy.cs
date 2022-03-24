 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
/// <summary>
/// 風力
/// </summary>
public class WindStrategy : GeneratorStrategy
{
    private Generator generator;
    private string name = "風力発電所";
    private int interval;
    private int hp = 3;
    private float rad;

    public WindStrategy()
    {
        this.interval = 3;
        this.rad = -Mathf.PI/4;
    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        if (Generator.GetPhase() == 0)
        {
            this.rad += Mathf.PI/4;
        }
        else
        {
            this.rad += Mathf.PI/8;
        }
        this.generator.Shot(Mathf.Cos(this.rad), Mathf.Sin(this.rad), 1, 10);
    }
    /// <summary>
    /// 発電所の射撃間隔の確定
    /// </summary>
    public void DefineInterval()
    {
        if (generator == null)
        {
            return;
        }

        if (Generator.GetPhase() == 0)
        {
            this.interval = 3;
        }
        else if (Generator.GetPhase() == 1)
        {
            this.interval = 2;
        }
        else if (Generator.GetPhase() == 2)
        {
            this.interval = 1;
        }
        this.generator.SetInterval(this.interval);
    }
    /// <summary>
    /// 発電所のHPの確定
    /// </summary>
    public void DefineHP()
    {
        if (generator == null)
        {
            return;
        }

        this.generator.SetHP(this.hp);
    }
    /// <summary>
    /// 発電所の名前のゲッター
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return this.name;
    }
    /// <summary>
    /// 発電所のセッター
    /// </summary>
    /// <param name="generator"></param>
    public void SetGenerator(Generator generator)
    {
        this.generator = generator;
    }
}