using UnityEngine;
using TMPro;
using System.Linq;

public class FrameRateDropdown : MonoBehaviour
{
    public TMP_Dropdown frameRateDropdown;
    
    private readonly int[] baseRates = { 30, 60, 120, 144, 165, 240 };
    private int[] availableRefreshRates;

    void Start()
    {
        int maxSupportedRate = Mathf.RoundToInt((float)Screen.resolutions
            .Max(r => r.refreshRateRatio.value));
        
        availableRefreshRates = baseRates
            .Where(rate => rate <= maxSupportedRate)
            .Concat(new[] { maxSupportedRate })
            .Distinct()
            .OrderByDescending(rate => rate)
            .ToArray();

        InitializeFrameRateOptions();
        frameRateDropdown.onValueChanged.AddListener(SetTargetRefreshRate);
    }

    void InitializeFrameRateOptions()
    {
        frameRateDropdown.ClearOptions();
        
        var options = availableRefreshRates
            .Select(rate => $"{rate}FPS")
            .ToList();
        
        frameRateDropdown.AddOptions(options);
        
        frameRateDropdown.value = GetCurrentRefreshRateIndex();
        frameRateDropdown.RefreshShownValue();
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
