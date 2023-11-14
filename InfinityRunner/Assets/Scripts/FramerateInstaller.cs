using UnityEngine;

public class FramerateInstaller : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
#endif
    }
}
