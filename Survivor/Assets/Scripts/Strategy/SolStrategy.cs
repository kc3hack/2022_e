/// <summary>
/// 太陽光
/// </summary>
/// <author>tosshi</author>
public class SolStrategy : GeneratorStrategy
{
    private Generator generator;
    private string name = "太陽光発電所";
    private int interval = 5;
    private int hp = 3;
    private int solnum = 0;

    public SolStrategy()
    {
        solnum = Generator.GetWeather();
    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        if (Generator.GetPhase() == 0)
        {
            this.generator.Shot(1, 0, 0, 10); // 右向きに射撃
            this.generator.Shot(-1, 0, 0, 10); // 左向きに射撃
        }
        else if (Generator.GetPhase() == 1)
        {
            this.generator.Shot(); // 上向きに射撃
            this.generator.Shot(1.7f, -1, 150, 10); // 右下向きに射撃
            this.generator.Shot(-1.7f, -1, 30, 10); // 左下向きに射撃
        }
        else
        {
            this.generator.Shot(); // 上向きに射撃
            this.generator.Shot(1, 0, 0, 10); // 右向きに射撃
            this.generator.Shot(-1, 0, 0, 10); // 左向きに射撃
            this.generator.Shot(0, -1, 90, 10); // 下向きに射撃
        }
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
        else if (solnum == 0)
        {
            this.generator.SetInterval(this.interval + 5);
        }
        else if (solnum == 1)
        {
            this.generator.SetInterval(this.interval + 1);
        }
        else if (solnum == 2)
        {
            this.generator.SetInterval(this.interval - 1);
        }
        else if (solnum == 3)
        {
            this.generator.SetInterval(this.interval + 7);
        }

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