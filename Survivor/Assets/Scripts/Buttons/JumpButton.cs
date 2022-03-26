using UnityEngine;

/// <summary>
/// シーン遷移ボタン
/// </summary>
public class JumpButton : MonoBehaviour, IClickable
{
    /// <summary>
    /// 遷移先のシーン名
    /// </summary>
    [SerializeField] private string sceneName;

    /// <summary>
    /// クリック時処理、シーン遷移
    /// </summary>
    public void OnClicked()
    {
        Debug.Log("jump to " + sceneName);
        GameManager.GetInstance().JumpScene(sceneName);
    }
}
