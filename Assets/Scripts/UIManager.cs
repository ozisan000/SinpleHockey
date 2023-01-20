using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    const float transparent = 0.0f;
    const float nonTransparent = 1.0f;

    [SerializeField]
    float uiFadeSpeed = 5.0f;
    [SerializeField]
    List<CanvasGroup> uiList = new List<CanvasGroup>();
    [SerializeField]
    TextMeshProUGUI timeText;
    int uiFLag = 0;

    public void Init()
    {
        
    }

    public void UIUpdate(int flag)
    {
        switch (flag)
        {

        }
        if (uiFLag != flag)
        {
            ChangeUI(flag, uiFLag);
        }
    }

    private void ChangeUI(int flag,int jump_flag)
    {
        UILib.WaitGroupFadeProgress(
            uiList[flag],
            EasingType.OUT_QUAD,
            transparent,
            uiFadeSpeed,
            () => { return true; },
            () =>{
                UILib.WaitGroupFadeProgress(
                        uiList[flag],
                        EasingType.OUT_QUAD,
                        nonTransparent,
                        uiFadeSpeed,
                        () => { return true; },
                        null
                    );
            }
        );
    }
}
