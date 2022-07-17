using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framerate : MonoBehaviour
{
    public FramerateEnum framerate = FramerateEnum.Fps60;

    public enum FramerateEnum
    {
        Fps60,
        Fps120,
        Fps144
    }

    // Start is called before the first frame update
    void Awake()
    {
        switch (framerate)
        {
            case FramerateEnum.Fps144:
                Application.targetFrameRate = 144;
                break;
            case FramerateEnum.Fps120:
                Application.targetFrameRate = 120;
                break;
            default:
                Application.targetFrameRate = 60;
                break;
        }
        
        DontDestroyOnLoad(this);
    }
}