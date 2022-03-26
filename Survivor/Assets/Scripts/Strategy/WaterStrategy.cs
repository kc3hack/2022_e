/// <summary>
/// 水力
/// </summary>
public class WaterStrategy : GeneratorStrategy
{
    private Generator generator;
    private string name = "水力発電所";
    private int interval = 5;
    private int hp = 3;
    private int num; // 発射回数

    public WaterStrategy()
    {
        int interval = 10;
        switch (Generator.GetWeather())
        {
            case 0:
                interval = 8;
                break;
            case 1:
            case 2:
                interval = 12;
                break;
            case 3:
                interval = 5;
                break;
        }

        this.interval = interval;
        this.num = 0;
    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        if (Generator.GetPhase() < 2)
        {
            this.generator.Shot(); // 上向きに射撃
            this.generator.Shot(3, 1, 33.33f, 3);
            this.generator.Shot(-3, 1, 153.33f, 3);
            this.generator.Shot(3, -1, -33.33f, 3);
            this.generator.Shot(-3, -1, -153.33f, 3);
            this.generator.Shot(0, -1, -90, 10);
        }
        else if (Generator.GetPhase() < 4)
        {
            this.num += 1;
            if (num % 2 == 0)
            {
                this.generator.Shot(1, 0, 0, 10);
                this.generator.Shot(1, 3, 60, 3);
                this.generator.Shot(-1, 3, 120, 3);
                this.generator.Shot(-1, -0, 180, 10);
                this.generator.Shot(-1, -3, 240, 3);
                this.generator.Shot(1, -3, 300, 3);
            }
            else
            {
                this.generator.Shot(); // 上向きに射撃
                this.generator.Shot(3, 1, 33.33f, 3);
                this.generator.Shot(-3, 1, 153.33f, 3);
                this.generator.Shot(3, -1, -33.33f, 3);
                this.generator.Shot(-3, -1, -153.33f, 3);
                this.generator.Shot(0, -1, -90, 10);
            }
        }
        else
        {
            this.num += 1;
            if (num % 4 == 0)
            {
                this.generator.Shot(3, 1, 30, 3);
                this.generator.Shot(1, 3, 60, 3);
                this.generator.Shot(-3, -1, 30, 3);
                this.generator.Shot(-1, -3, 60, 3);
                this.generator.Shot(1, -1, -45, 10);
                this.generator.Shot(-1, 1, 135, 10);
            }
            else if (num % 4 == 1)
            {
                this.generator.Shot(1, 0, 0, 10);
                this.generator.Shot(1, 3, 60, 3);
                this.generator.Shot(-1, 3, 120, 3);
                this.generator.Shot(-1, -0, 180, 10);
                this.generator.Shot(-1, -3, 240, 3);
                this.generator.Shot(1, -3, 300, 3);
            }
            else if (num % 4 == 2)
            {
                this.generator.Shot(3, -1, -30, 3);
                this.generator.Shot(1, -3, -60, 3);
                this.generator.Shot(-3, 1, -30, 3);
                this.generator.Shot(-1, 3, -60, 3);
                this.generator.Shot(1, 1, 45, 10);
                this.generator.Shot(-1, -1, 45, 10);
            }
            else
            {
                this.generator.Shot(); // 上向きに射撃
                this.generator.Shot(3, 1, 33.33f, 3);
                this.generator.Shot(-3, 1, 153.33f, 3);
                this.generator.Shot(3, -1, -33.33f, 3);
                this.generator.Shot(-3, -1, -153.33f, 3);
                this.generator.Shot(0, -1, -90, 10);
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