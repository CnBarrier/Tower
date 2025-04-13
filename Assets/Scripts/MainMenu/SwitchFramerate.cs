using UnityEngine;
using TMPro;
using System.Linq;

public class SwitchFramerate : MonoBehaviour
{
    public TMP_Dropdown Framerates;
    
    private readonly int[] baseRates = { 30, 60, 120, 144, 165, 240 };
    private int[] availableRefreshRates;

    void Start()
    {
        InitializeFrameRateSettings();
        Framerates.onValueChanged.AddListener(SetTargetRefreshRate);
    }

    void InitializeFrameRateSettings()
    {
        int maxSupportedRate = CalculateMaxSupportedRefreshRate();
        availableRefreshRates = GenerateAvailableRates(maxSupportedRate);
        InitializeFrameRateOptions();
    }

    int CalculateMaxSupportedRefreshRate()
    {
        return Mathf.RoundToInt((float)Screen.resolutions
            .Max(r => r.refreshRateRatio.value));
    }

    int[] GenerateAvailableRates(int maxRate)
    {
        return baseRates
            .Where(rate => rate <= maxRate)
            .Append(maxRate)
            .Distinct()
            .OrderByDescending(rate => rate)
            .ToArray();
    }

    void InitializeFrameRateOptions()
    {
        Framerates.ClearOptions();
        
        var options = availableRefreshRates
            .Select(rate => $"{rate}FPS")
            .ToList();
        
        Framerates.AddOptions(options);
        
        Framerates.value = GetCurrentRefreshRateIndex();
        Framerates.RefreshShownValue();
    }

    int GetCurrentRefreshRateIndex()
    {
        int current = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value);
        for (int i = 0; i < availableRefreshRates.Length; i++)
        {
            if (availableRefreshRates[i] == current) return i;
        }
        return 0;
    }

    void SetTargetRefreshRate(int index)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = availableRefreshRates[index] == -1 ? 
            -1 :
            availableRefreshRates[index];
    }
}
