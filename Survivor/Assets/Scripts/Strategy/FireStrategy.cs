/// <summary>
/// 火力
/// </summary>
public class FireStrategy : GeneratorStrategy
{
    private Generator generator;
    private string name = "火力発電所";
    private int interval = 5;
    private int hp = 3;
    private int fossil;

    public FireStrategy()
    {
        fossil = Generator.GetFossil();
    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        if(fossil > 0){
            fossil--;
            this.generator.Shot(0, 1, 90, 10);  // 上向きに射撃
            this.generator.Shot(0, -1, 90, 10); // 下向きに射撃
            this.generator.Shot(1, 0, 0, 10);   // 右向きに射撃
            this.generator.Shot(-1, 0, 0, 10);  // 左向きに射撃
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