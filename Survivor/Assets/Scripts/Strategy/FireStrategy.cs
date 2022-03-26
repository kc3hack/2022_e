/// <summary>
/// 火力
/// </summary>
/// 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStrategy : GeneratorStrategy
{
    private Generator generator;
    private string name = "火力発電所";
    private int interval = 4;
    private int hp = 3;
    private float n = 0;
    private int fossil;

    public FireStrategy()
    {

    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        if(fossil > 0){
            fossil--;
            this.generator.Shot(Mathf.Cos(Rand(n+90)), Mathf.Sin(Rand(n+90)), 90+n,10); // 上向きに射撃
            this.generator.Shot(Mathf.Cos(Rand(n-90)), Mathf.Sin(Rand(n-90)), 90+n,10); // 下向きに射撃
            this.generator.Shot(Mathf.Cos(Rand(n)),    Mathf.Sin(Rand(n)),    0+n, 10); // 右向きに射撃
            this.generator.Shot(Mathf.Cos(Rand(n+180)),Mathf.Sin(Rand(n+180)),0+n, 10); // 左向きに射撃
            n = n + 15;
        }
        else{
            this.generator.Shot(Mathf.Cos(Rand(n+90)), Mathf.Sin(Rand(n+90)), 90+n,10); // 上向きに射撃
            n = n + 45;
        }
    }
    public float Rand(float n)
    {
        return n * (3.14f / 180f);
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