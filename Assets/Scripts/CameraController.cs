using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Camera camera;
    [SerializeField]private float CameraOrthographicSize;
    
    public static CameraController instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        camera.orthographicSize = CameraOrthographicSize * (0.5625f / camera.aspect);
    }
}
