using Cinemachine;
using UnityEngine;

/// <summary>
/// �J�����̃^�C�v
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
    CinemachineVirtualCamera titleCamera;       //�^�C�g��VCam
    [SerializeField]
    CinemachineVirtualCamera playerCamera;      //�v���C���[VCam
    [SerializeField]
    CinemachineVirtualCamera playerJumpCamera;      //�v���C���[���W�����v�����Ƃ���VCam
    [SerializeField]
    CinemachineVirtualCamera playerSlidingCamera;      //�v���C���[���W�����v�����Ƃ���VCam
    [SerializeField]
    CinemachineVirtualCamera playerBreakCamera;      //�v���C���[���ǂ��󂵂��Ƃ���VCam
    [SerializeField]
    CinemachineVirtualCamera resultCamera;
    [SerializeField]
    CameraType cameraType = CameraType.TitleCamera;     //���ݗL���ɂȂ��Ă���J����
    [SerializeField]
    float blendSpeed = 2.0f;            //�u�����h�X�s�[�h

    float defaultBlendSpeed;            //�f�t�H���g�̃u�����h�X�s�[�h

    CinemachineBrain cinemachineCamera;

    public CinemachineBrain MainCameraBrain { get => cinemachineCamera; }
    public float DefaultBlendSpeed { get => defaultBlendSpeed; }
    public float BlendSpeed { get => blendSpeed; set => blendSpeed = value; }

    const int cInValid = 0;      //�J�����𖳌��ɂ���
    const int cValid = 1;        //�J������L���ɂ���

    /// <summary>
    /// �J������L���ɂ���
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
    /// ���ݗL���ɂȂ��Ă���J�����̃^�C�v�ɍ��킹�ėD��x��ύX����
    /// </summary>
    private void ChangeVCam()
    {
        //�D��x���X�C�b�`
        switch (cameraType)
        {
            case CameraType.TitleCamera:
                //�^�C�g���J������L���ɂ���
                titleCamera.Priority = cValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cInValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.PlayerCamera:
                //�v���C���[�J������L���ɂ���
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cInValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.JumpCamera:
                //�W�����v�J������L���ɂ���
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cValid;
                playerSlidingCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.SlidingCamera:
                //�X���C�f�B���O�J������L���ɂ���
                titleCamera.Priority = cInValid;
                playerCamera.Priority = cInValid;
                playerJumpCamera.Priority = cInValid;
                playerSlidingCamera.Priority = cValid;
                resultCamera.Priority = cInValid;
                playerBreakCamera.Priority = cInValid;
                break;
            case CameraType.ResultCamera:
                //�X���C�f�B���O�J������L���ɂ���
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
        //// �A�X�y�N�g��Œ�
        //var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
        //var width = (this.baseWidth * scale) / Screen.width;
        //var height = (this.baseHeight * scale) / Screen.height;
        //mainCam.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);
        cinemachineCamera = Camera.main.GetComponent<CinemachineBrain>();
        defaultBlendSpeed = cinemachineCamera.m_DefaultBlend.m_Time;
        cinemachineCamera.m_DefaultBlend.m_Time = blendSpeed;
        //�J�����̗D��x���Z�b�g
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
