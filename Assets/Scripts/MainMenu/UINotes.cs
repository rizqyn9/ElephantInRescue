using System.Collections;
using System.Collections.Generic;
using EIR.MainMenu;
using UnityEngine;

public class UINotes : MonoBehaviour
{
    [SerializeField] UI_MainMenu uI_MainMenu;
    [SerializeField] GameObject primaryContainer;
    [SerializeField] GameObject _containerState;

    private GameObject containerState
    {
        get => _containerState;
        set
        {
            if (_containerState == value) return;
            handleOnContainerChanged(_containerState, value);
            _containerState = value;
        }
    }

    private void OnEnable()
    {
        containerState = primaryContainer;
    }

    public void OnChange(GameObject _containerOpen)
    {
        containerState = _containerOpen;
    }

    public void handleOnContainerChanged(GameObject _before, GameObject _after)
    {
        if (_before != null) _before.SetActive(false);
        _after.SetActive(true);
    }

    public void On_BackContainer()
    {
        if (primaryContainer.name == _containerState.name) uI_MainMenu.OnClick_Overlay();
        else OnChange(primaryContainer);
    }
}
