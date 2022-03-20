/// <summary>
/// 発電機の特性のインタフェース
/// 未確定なのでこれから作る部分
/// </summary>
public interface GeneratorStrategy
{
    void Attack();
    void DefineInterval();
    void DefineHP();
    string GetName();
    void SetGenerator(Generator generator);
}