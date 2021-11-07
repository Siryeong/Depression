using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Looxid.Link;

public class Lab : MonoBehaviour
{

    public static Lab Instance { get; private set; }
    void Awake() => Instance = this;

    public Image statusImg;

    public Color positiveColor = Color.green;
    public Color negativeColor = Color.red;

    enum dataTypes
    {
        rawSignal,
        filteredRawSignal,
        featuredIndexAlpha
    }

    FileStream[] fileStreams = new FileStream[3];
    StreamWriter[] streamWriters = new StreamWriter[3];
    private string filePath = "C:\\Users\\DELL\\Desktop\\Data\\";
    private string[] fileTitle = {"Raw", "FilteredRaw", "Alpha"};
    private string[] fileNames = new string[3];
    private float alphaAsymmetryValue = 0f;
    public float getAsymmetryValue { get => alphaAsymmetryValue; }

    private void Start()
    {
        statusImg.color = Color.white;
    }

    private void FixedUpdate()
    {
        if(alphaAsymmetryValue > 0f)
            statusImg.color = positiveColor;
        else
            statusImg.color = negativeColor;
        
    }

    private void OnEnable()
    {
        OpenFileStream();
        LooxidLinkManager.Instance.SetDebug(true);
        LooxidLinkManager.Instance.Initialize();
        LooxidLinkData.OnReceiveEEGFeatureIndexes += OnReceiveEEGFeatureIndexes;
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
    }

    private void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGFeatureIndexes -= OnReceiveEEGFeatureIndexes;
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
        LooxidLinkManager.Instance.Terminate();
        CloseFileStream();
    }

    void OnReceiveEEGFeatureIndexes(EEGFeatureIndex featureIndex)
    {
        string outputText 
            = featureIndex.Alpha(EEGSensorID.AF3) + ","
            + featureIndex.Alpha(EEGSensorID.AF4) + ","
            + featureIndex.Alpha(EEGSensorID.Fp1) + ","
            + featureIndex.Alpha(EEGSensorID.Fp2) + ","
            + featureIndex.Alpha(EEGSensorID.AF7) + ","
            + featureIndex.Alpha(EEGSensorID.AF8);
        streamWriters[(int)dataTypes.featuredIndexAlpha].WriteLine(outputText);

        alphaAsymmetryValue = (float)(featureIndex.Alpha(EEGSensorID.AF4) - featureIndex.Alpha(EEGSensorID.AF3));

    }

    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData)
    {
        string outputText;

        // Raw Signal
        for(int i = 0; i < rawSignalData.rawSignal.Count; i++)
        {
            outputText 
                = rawSignalData.rawSignal[i].ch_data[(int)EEGSensorID.AF3] + ","
                + rawSignalData.rawSignal[i].ch_data[(int)EEGSensorID.AF4] + ","
                + rawSignalData.rawSignal[i].ch_data[(int)EEGSensorID.Fp1] + ","
                + rawSignalData.rawSignal[i].ch_data[(int)EEGSensorID.Fp2] + ","
                + rawSignalData.rawSignal[i].ch_data[(int)EEGSensorID.AF7] + ","
                + rawSignalData.rawSignal[i].ch_data[(int)EEGSensorID.AF8];
            streamWriters[(int)dataTypes.rawSignal].WriteLine(outputText);
        }

        // filtered Raw Signal
        for (int i = 2000 - rawSignalData.rawSignal.Count; i < 2000; i++)
        {
            outputText 
                = rawSignalData.FilteredRawSignal(EEGSensorID.AF3)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF4)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.Fp1)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.Fp2)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF7)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF8)[i];
            streamWriters[(int)dataTypes.filteredRawSignal].WriteLine(outputText);
        }
    }

    private void OpenFileStream()
    {
        for (int i = 0; i < 3; i++)
        {
            fileNames[i] = filePath + DateTime.Now.ToString("yyMMdd_HHmm") + fileTitle[i] + ".csv";
            fileStreams[i] = new FileStream(fileNames[i], FileMode.Create);
            streamWriters[i] = new StreamWriter(fileStreams[i]);
        }
    }

    private void CloseFileStream()
    {
        for(int i = 0; i < 3; i++)
        {
            streamWriters[i].Close();
        }
    }
}
