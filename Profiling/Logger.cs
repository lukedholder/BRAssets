using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;
using System.IO;

public class Logger : MonoBehaviour
{
    private string filePath;
    private ProfilerRecorder cpuRecorder;

    //public string marker = "enemyMove";


    //private ProfilerRecorder varName;
    //private ProfilerRecorder fpsTankTest;
    private static ProfilerRecorder test;
    public static readonly ProfilerMarker marker = new ProfilerMarker("marker");
     
    void Start()
    {
        //Debug.Log("Make CSV");
        filePath = Path.Combine(Application.persistentDataPath, "logs.csv");
        List<string> csvLines = new List<string>();
        csvLines.Add($"{null}, {null},{null}");
        File.AppendAllLines(filePath, csvLines);
    }

    void OnEnable()
    {
        cpuRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "PlayerLoop");

        //varName = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "<markerName>");
        //fpsTankTest = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "fpsTank");
        test = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "marker");
        //Debug.Log(test.LastValue);
    }

    public static void Log(string name) {//, ProfilerRecorder methodTest) {
        Debug.Log("LOG method accessed");
        //test = ProfilerRecorder.StartNew(ProfilerCategory.Scripts, "stressor");
        //test = methodTest;
        
    }

    void Update()
    {
        main();
    }

    public void main() {
        float cpuTime = GetTimeMs(cpuRecorder);

        float testTime = GetTimeMs(test);

        //Debug.Log(testTime);
        
        if (testTime > 0) {
            Debug.Log(testTime);
            WriteDataToCSV("function", testTime, cpuTime);
        }
    }

    private float GetTimeMs(ProfilerRecorder temp) {
        if (temp.Valid && temp.Count > 0) {
            float tempTime = temp.LastValue * 1e-6f;
            return tempTime;
        }
        return 0;
    }

    void OnDisable()
    {
        cpuRecorder.Dispose();

        //varName.Dispose();
        test.Dispose();
    }

    void WriteDataToCSV(string functionName, float testTime, float cpuTime) {
        List<string> csvLines = new List<string>();
        csvLines.Add($"{functionName}, {testTime}, {cpuTime}");
        File.AppendAllLines(filePath, csvLines);
    }

    void OnApplicationQuit() {
        List<string> csvLines = new List<string>();
        csvLines.Add($"{"AVG"}, {""}, {""}");
        File.AppendAllLines(filePath, csvLines);
    }
}
