using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : MenuButtonBase
{
    protected override void Click()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
