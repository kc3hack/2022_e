/// <summary>
/// 発電所戦略サンプル2
/// 戦略追加の手順
/// 1. Strategy/ にGeneratorStrategyを実装したクラスを作成
/// 2. GameManagerのSelectStrategyメソッド内のswitch文内に追記
/// </summary>
public class SimpleStrategy2 : GeneratorStrategy
{
    private Generator generator;
    private string name = "サンプル発電所2";
    private int interval = 5;
    private int hp = 3;

    public SimpleStrategy2()
    {

    }

    /// <summary>
    /// 攻撃内容
    /// </summary>
    public void Attack()
    {
        this.generator.Shot(0, -1, 90, 10); // 下向きに射撃
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