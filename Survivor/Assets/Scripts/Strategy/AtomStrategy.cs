/// <summary>
/// 原子力
/// </summary>
public class AtomStrategy : GeneratorStrategy
{
    private Generator generator;
    private string name = "原子力発電所";
    private int interval = 5;
    private int hp = 3;
    private int num;

    public AtomStrategy()
    {
        this.num = 0;
    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        if (Generator.GetPhase() == 0)
        {
            this.generator.Shot(3, 1, 33.33f, 3);
            this.generator.Shot(-3, 1, 153.33f, 3);
            this.generator.Shot(0, -1, -90, 10);
        }
        else if (Generator.GetPhase() == 1)
        {
            this.generator.Shot();
            this.generator.Shot(3, -1, -30, 3);
            this.generator.Shot(-3, -1, 210, 3);
        }
        else if (Generator.GetPhase() == 2)
        {
            this.num += 1;
            if (this.num % 4 == 0)
            {
                // this.generator.Shot(0, 1, 90, 10);
                this.generator.Shot(1, 0, 0, 10);
                this.generator.Shot(-1, 0, 0, 10);
                this.generator.Shot(0, -1, 90, 10);
            }
            else if (this.num % 4 == 1)
            {
                this.generator.Shot(0, 1, 90, 10);
                // this.generator.Shot(1, 0, 0, 10);
                this.generator.Shot(-1, 0, 0, 10);
                this.generator.Shot(0, -1, 90, 10);
            }
            else if (this.num % 4 == 2)
            {
                this.generator.Shot(0, 1, 90, 10);
                this.generator.Shot(1, 0, 0, 10);
                // this.generator.Shot(-1, 0, 0, 10);
                this.generator.Shot(0, -1, 90, 10);
            }
            else if (this.num % 4 == 3)
            {
                this.generator.Shot(0, 1, 90, 10);
                this.generator.Shot(1, 0, 0, 10);
                this.generator.Shot(-1, 0, 0, 10);
                // this.generator.Shot(0, -1, 90, 10);
            }
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