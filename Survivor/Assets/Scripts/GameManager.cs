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

    public void SelectStrategy()
    {
        this.strategy = new SimpleStrategy();
    }
}
