using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOperate : MonoBehaviour
{
    private bool isPanelShown = false;
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(closePanel);
    }

    public void showPanel()
    {
        if (!isPanelShown)
        {
            gameObject.SetActive(true);
            isPanelShown = true;
        }
    }

    private void closePanel()
    {
        if (isPanelShown)
        {
            gameObject.SetActive(false);
            isPanelShown = false;
        }
    }
}
