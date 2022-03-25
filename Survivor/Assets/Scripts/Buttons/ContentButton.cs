using UnityEngine;

public class ContentButton : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        this.gameObject.SetActive(false);
    }
}