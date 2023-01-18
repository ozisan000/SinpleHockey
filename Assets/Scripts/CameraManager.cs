using Cinemachine;
using UnityEngine;

/// <summary>
/// カメラのタイプ
/// </summary>
public enum CameraType
{
    TitleCamera,
    PlayerCamera,
    JumpCamera,
    SlidingCamera,
    ResultCamera,
    BreakCamera
}

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    float baseWidth = 9.0f;
    [SerializeField]
    float baseHeight = 16.0f;
    [SerializeField]
    CinemachineVirtualCamera titleCamera;       //タイトルVCam
    [SerializeField]
    CinemachineVirtualCamera playerCamera;      //プレイヤーVCam
    [SerializeField]
    CinemachineVirtualCamera playerJumpCamera;      //プレイヤーがジャンプしたときのVCam
    [SerializeField]
    CinemachineVirtualCamera playerSlidingCamera;      //プレイヤーがジャンプしたときのVCam
    [SerializeField]
    CinemachineVirtualCamera playerBreakCamera;      //プレイヤーが壁を壊したときのVCam
    [SerializeField]
    CinemachineVirtualCamera resultCamera;
    [SerializeField]
    CameraType cameraType = CameraType.TitleCamera;     //現在有効になっているカメラ
    [SerializeField]
    float blendSpeed = 2.0f;            //ブレンドスピード

    float defaultBlendSpeed;            //デフォルトのブレンドスピード

    CinemachineBrain cinemachineCamera;

    public CinemachineBrain MainCameraBrain { get => cinemachineCamera; }
    public float DefaultBlendSpeed { get => defaultBlendSpeed; }
    public float BlendSpeed { get => blendSpeed; set => blendSpeed = value; }

    const int cInValid = 0;      //カメラを無効にする
    const int cValid = 1;        //カメラを有効にする

    /// <summary>
    /// カメラを有効にする
    /// </summary>
    public CameraType ChangeCamera
    {
        set
        {
            cameraType = value;
            ChangeVCam();
        }
    }

    /// <summary>
    /// 現在有効になっているカメラのタイプに合わせて優先度を変更する
    /// </summary>
    private void ChangeVCam()
    {
        //優先度をスイッチ
        switch (cameraType)
        {
            case CameraType.TitleCamera:
                //タイトルカメラを有効にする
                titleCamera.Priority = cValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cInValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.PlayerCamera:
                //プレイヤーカメラを有効にする
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cInValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.JumpCamera:
                //ジャンプカメラを有効にする
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cValid;
                playerSlidingCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.SlidingCamera:
                //スライディングカメラを有効にする
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.ResultCamera:
                //スライディングカメラを有効にする
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cInValid;
                resultCamera.Priority = cValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.BreakCamera:
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cInValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cValid;
                break;
        }
    }
    public void MyAwake()
    {
        //var mainCam = Camera.main;
        //// アスペクト比固定
        //var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
        //var width = (this.baseWidth * scale) / Screen.width;
        //var height = (this.baseHeight * scale) / Screen.height;
        //mainCam.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);
        cinemachineCamera = Camera.main.GetComponent<CinemachineBrain>();
        defaultBlendSpeed = cinemachineCamera.m_DefaultBlend.m_Time;
        cinemachineCamera.m_DefaultBlend.m_Time = blendSpeed;
        //カメラの優先度をセット
        ChangeVCam();
    }

    public void MyDestroy()
    {
    }

    public void MyFixedUpdate()
    {
    }

    public void MyStart()
    {
    }

    public void MyUpdate()
    {
    }
}
