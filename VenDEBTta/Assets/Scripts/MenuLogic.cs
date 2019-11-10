using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogic : MonoBehaviour
{

    public CanvasGroup mainMenu;
    public CanvasGroup helpMenu;

    private void Start()
    {
        CloseCanvasGroup(helpMenu);
        OpenCanvasGroup(mainMenu);
    }

    private void OpenCanvasGroup(CanvasGroup group)
    {
        group.alpha = 1f;
        group.interactable = true;
        group.blocksRaycasts = true;
    }
    private void CloseCanvasGroup(CanvasGroup group)
    {
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void OpenMain()
    {
        CloseCanvasGroup(helpMenu);
        OpenCanvasGroup(mainMenu);
    }

    public void OpenHelp()
    {
        CloseCanvasGroup(mainMenu);
        OpenCanvasGroup(helpMenu);
    }
}
