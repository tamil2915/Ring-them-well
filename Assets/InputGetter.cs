using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputGetter : MonoBehaviour
{
    MouseCtrl mouseCtrl;

    private void OnEnable()
    {
        mouseCtrl.Enable();
    }

    private void OnDisable()
    {
        mouseCtrl.Disable();
    }

    private void Awake()
    {
        mouseCtrl = new MouseCtrl();

        mouseCtrl.MouseActionmap.leftClick.performed += ctx => leftClick();

    }

    public void leftClick()
    {
    }
}
