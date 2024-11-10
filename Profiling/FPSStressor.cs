using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;

public class FPSStressor : MonoBehaviour
{
    //fpsTank

    
    void Update()
    {
        //myMethodMarker.Begin();
        stressor();
        //myMethodMarker.End();
    }

    private static readonly ProfilerMarker stressorMarker = new ProfilerMarker("stressor");
    /*
    private ProfilerRecorder test;
    void OnEnable() {
        test = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "stressor");
    }
    */
    private void stressor() {
        stressorMarker.Begin();
        for (int i = 0; i < 10000000; i++) {
            Mathf.Sqrt(i);
        }
        stressorMarker.End();
        //Logger.Log("stressor");//, test);
    }
}
