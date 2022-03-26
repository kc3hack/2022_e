using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の管理人
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// シングルトン
    /// </summary>
    private static GameManager instance;

    private GeneratorStrategy strategy; // 使用する戦略(発電所の種類) メインメニューで決定する

    /// <summary>
    /// シングルトンゲッター
    /// </summary>
    /// <returns></returns>
    public static GameManager GetInstance()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager"))
        {
            Debug.Log("GameManagerを生成します");
            instance = Instantiate(Resources.Load<GameObject>("Prefabs/GameManager")).GetComponent<GameManager>();
        }
        return instance;
    }

    /// <summary>
    /// ゲーム終了時処理
    /// </summary>
    public void GameFinish()
    {

    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    public void JumpScene(string sceneName)
    {
        Debug.Log("シーン" + sceneName + "に移動します");
        SceneManager.LoadScene(sceneName);
    }

    public void SelectStrategy(string strategyname)
    {
        GeneratorStrategy strategy = new SimpleStrategy();
        switch (strategyname)
        {
            case "Simple":
                strategy = new SimpleStrategy();
                break;
            case "Simple2":
                strategy = new SimpleStrategy2();
                break;
            case "Atom":
                strategy = new AtomStrategy();
                break;
            case "Fire":
                strategy = new FireStrategy();
                break;
            case "Sol":
                strategy = new SolStrategy();
                break;
            case "Water":
                strategy = new WaterStrategy();
                break;
            case "Wind":
                strategy = new WindStrategy();
                break;
        }
        Debug.Log("Strategy selected : " + strategy.GetName());
        this.strategy = strategy;
    }

    public GeneratorStrategy GetStrategy()
    {
        return this.strategy;
    }
}
