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

    public SolStrategy()
    {

    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        this.generator.Shot(); // 上向きに射撃
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