using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Looxid.Link;

public class BrainStatusUI : MonoBehaviour
{
    [Header("Colors")]
    public Color ConnectedColor = Color.white;
    public Color DisconnectedColor = Color.white;

    [Header("Device Status")]
    public Image[] SensorStatusImage;

    [Header("Raw Signal")]
    public LineChart[] RawSignalCharts;
    

    private EEGSensor sensorStatusData;

    private void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGSensorStatus += OnReceiveEEGSensorStatus;
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
        StartCoroutine(DisplayData());
    }

    private void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGSensorStatus -= OnReceiveEEGSensorStatus;
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
    }

    void OnReceiveEEGSensorStatus(EEGSensor sensorStatusData)
    {
        this.sensorStatusData = sensorStatusData;
    }

    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData)
    {
        int numChannel = System.Enum.GetValues(typeof(EEGSensorID)).Length;
        for (int i = 0; i < numChannel; i++)
        {
            RawSignalCharts[i].SetValue(rawSignalData.FilteredRawSignal((EEGSensorID)i));
        }
    }


    IEnumerator DisplayData()
    {
        while(this.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.1f);

            // Device Status: Sensor Status
            if (sensorStatusData != null)
            {
                int numChannel = System.Enum.GetValues(typeof(EEGSensorID)).Length;
                for (int i = 0; i < numChannel; i++)
                {
                    bool isSensorOn = sensorStatusData.IsSensorOn((EEGSensorID)i);
                    SensorStatusImage[i].color = isSensorOn ? ConnectedColor : DisconnectedColor;
                }
            }
        }
    }
}
