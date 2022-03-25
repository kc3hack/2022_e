using UnityEngine;

public class GameSetButton : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        Generator.GameSet();
    }
}