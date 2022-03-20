using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour, IClickable
{
    [SerializeField] private string targetStrategyName;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<Text>().text = this.targetStrategyName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClicked()
    {
        GameManager.GetInstance().SelectStrategy(this.targetStrategyName);
    }
}
