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
    int uiID = -1;
    bool changeUIFlag = false;

    public void Init()
    {
        
    }

    public void UIUpdate(int id)
    {
        if (changeUIFlag) return;
        if (uiID != id)
        {
            changeUIFlag = true;
            Debug.Log($"ChangeUI:{id}");
            ChangeUI(uiID, id);
        }
        else
        {
            switch (uiID)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    break;
            }
        }
    }

    private void ChangeUI(int id,int jump_id)
    {
        UILib.WaitGroupFadeProgress(
            uiList[id+1],
            EasingType.OUT_QUAD,
            transparent,
            uiFadeSpeed,
            () => { return false; },
            () =>
            {
                UILib.WaitGroupFadeProgress(
                        uiList[jump_id + 1],
                        EasingType.OUT_QUAD,
                        nonTransparent,
                        uiFadeSpeed,
                        () => { return true; },
                        () => { uiID = jump_id;
                            changeUIFlag = false;
                        }
                    );
            }
        );
    }
}
