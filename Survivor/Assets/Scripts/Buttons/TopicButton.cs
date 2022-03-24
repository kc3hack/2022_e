using UnityEngine;

public class TopicButton : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject content;
    public void OnClicked()
    {
        this.content.SetActive(true);
    }
}