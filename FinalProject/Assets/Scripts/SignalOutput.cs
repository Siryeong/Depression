using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using Looxid.Link;

public class SignalOutput : MonoBehaviour
{
    FileStream fs;
    StreamWriter sw;

    FileStream fs2;
    StreamWriter sw2;

    void Awake()
    {
        // 파일 입출력
        openFileStream();
        openFileStreamOfAlpha();
    }


    void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
        LooxidLinkData.OnReceiveEEGFeatureIndexes += OnReceiveEEGFeatureIndexes;
    }

    void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
        LooxidLinkData.OnReceiveEEGFeatureIndexes -= OnReceiveEEGFeatureIndexes;
        sw.Close();
        sw2.Close();
    }

    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData) // raw signal 처리
    {
        // Raw signal CSV 파일로 출력하기
        string tmp;
        for(int i = 2000 - rawSignalData.rawSignal.Count; i < 2000; i++){
            tmp = rawSignalData.FilteredRawSignal(EEGSensorID.AF3)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF4)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.Fp1)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.Fp2)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF7)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF8)[i];
            sw.WriteLine(tmp);
        }
        

    }

    void OnReceiveEEGFeatureIndexes(EEGFeatureIndex featureIndex)
    {
        List<EEGFeatureIndex> datalist = LooxidLinkData.Instance.GetEEGFeatureIndexData(8.0f);
        string tmp;
        double left = featureIndex.Alpha(EEGSensorID.AF3);
        double right = featureIndex.Alpha(EEGSensorID.AF4);
        tmp = left + ","
            + right;
        sw.WriteLine(tmp);
    }

    void openFileStream()
    {
        string path = "C:\\Users\\DELL\\Desktop\\Data\\";
        string filename = path + DateTime.Now.ToString("yyMMdd_HHmm_Raw") + ".csv";
        fs = new FileStream(filename, FileMode.Create);
        sw = new StreamWriter(fs);
    }

    void openFileStreamOfAlpha()
    {
        string path = "C:\\Users\\DELL\\Desktop\\Data\\";
        string filename = path + DateTime.Now.ToString("yyMMdd_HHmm_Alpha.csv");
        fs2 = new FileStream(filename, FileMode.Create);
        sw2= new StreamWriter(fs2);
    }

}

