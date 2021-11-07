using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Looxid.Link;

public class LinkConnection : MonoBehaviour
{
    FileStream fs_Raw, fs2;
    StreamWriter sw_Raw, sw2;

    public double asymmetry_value { get; private set; }
    public double baseline_asymmetry_value { get; private set; }


    public static LinkConnection Instance { get; private set; }
    //void Awake() => Instance = this;
    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion    
    }

    void Start()
    {
        asymmetry_value = 0;
        baseline_asymmetry_value = 0;
    }

    void OnEnable()
    {
        OpenFileStreamForRawSignal(); // file open
        //OpenFileStreamForAlpha();
        LooxidLinkManager.Instance.Initialize();
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
        LooxidLinkData.OnReceiveEEGFeatureIndexes += OnReceiveEEGFeatureIndexes;
    }

    void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
        LooxidLinkData.OnReceiveEEGFeatureIndexes -= OnReceiveEEGFeatureIndexes;
        sw_Raw.Close(); // file close
        //sw2.Close();
        LooxidLinkManager.Instance.Terminate();
    }

    #region action delegate
    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData)
    {
        OutputToFileRawSignal(rawSignalData); // raw signal file output
    }

    void OnReceiveEEGFeatureIndexes(EEGFeatureIndex featureIndex)
    {
        updateAsymmetryValue(featureIndex);
        //OutputToFileAlpha(featureIndex);
    }

    #endregion

    #region FileIO
    void OpenFileStreamForRawSignal()
    {
        string path = "C:\\Users\\DELL\\Desktop\\Data\\";
        string filename = path + DateTime.Now.ToString("yyMMdd_HHmm") + "_Raw.csv";
        fs_Raw = new FileStream(filename, FileMode.Create);
        sw_Raw = new StreamWriter(fs_Raw);
    }

    void OpenFileStreamForAlpha()
    {
        string path = "C:\\Users\\DELL\\Desktop\\Data\\";
        string filename = path + DateTime.Now.ToString("yyMMdd_HHmm") + "_Alpha.csv";
        fs2 = new FileStream(filename, FileMode.Create);
        sw2 = new StreamWriter(fs2);
    }

    void OutputToFileRawSignal(EEGRawSignal rawSignalData)
    {
        string tmp;
        for (int i = 2000 - rawSignalData.rawSignal.Count; i < 2000; i++)
        {
            tmp = rawSignalData.FilteredRawSignal(EEGSensorID.AF3)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF4)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.Fp1)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.Fp2)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF7)[i] + ","
                + rawSignalData.FilteredRawSignal(EEGSensorID.AF8)[i];
            sw_Raw.WriteLine(tmp);
        }
    }

    void OutputToFileAlpha(EEGFeatureIndex featureIndex)
    {
    }
    #endregion

    void updateAsymmetryValue(EEGFeatureIndex featureIndex)
    {
        List<EEGFeatureIndex> datalist = LooxidLinkData.Instance.GetEEGFeatureIndexData(3.0f);

        double left = 0, right = 0;
        if (datalist.Count != 0)
        {
            foreach (EEGFeatureIndex item in datalist)
            {
                left += item.Alpha(EEGSensorID.AF3);
                right += item.Alpha(EEGSensorID.AF4);
            }
            left /= datalist.Count;
            right /= datalist.Count;
        }

        asymmetry_value = (right - left) / Abs(right + left);
        asymmetry_value -= baseline_asymmetry_value;
    }

    public void SetBaseline()
    {
        List<EEGFeatureIndex> datalist = LooxidLinkData.Instance.GetEEGFeatureIndexData(60f);
        double left = 0, right = 0;

        if (datalist.Count != 0)
        {
            foreach(EEGFeatureIndex item in datalist)
            {
                left += item.Alpha(EEGSensorID.AF3);
                right += item.Alpha(EEGSensorID.AF4);
            }
            left /= datalist.Count;
            right /= datalist.Count;
            baseline_asymmetry_value = (right-left) / Abs(right+left);
            Debug.Log("Baseline setted " + baseline_asymmetry_value);
        }
        else {
            Debug.LogError("Cannot set baseline");
            baseline_asymmetry_value = 0;
        }

    }


    #region Math

    double Abs(double x)
    {
        if (x > 0) return x;
        if (x < 0) return -x;
        return 0;
    }

    #endregion
}
