using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuButton : MenuButtonBase
{
    protected override void Click()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
