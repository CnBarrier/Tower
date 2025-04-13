using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class SwitchResolution : MonoBehaviour
{
    public TMP_Dropdown resolutions;
    
    private Resolution[] availableResolutions;
    
    void Start()
    {
        InitializeResolutions();
        SetupDropdown();
    }

    void InitializeResolutions()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        
        
        float currentRatio = (float)Screen.width / Screen.height;
        float targetRatio = Mathf.Abs(currentRatio - 16f/9) < Mathf.Abs(currentRatio - 16f/10) 
            ? 16f/9f
            : 16f/10f;                                                                          // 非16:9或16:10的分辨率不显示

        availableResolutions = Screen.resolutions
            .Select(r => new Resolution { width = r.width, height = r.height })
            .Distinct()
            .Where(r => 
            {
                float ratio = (float)r.width / r.height;
                return Mathf.Abs(ratio - targetRatio) < 0.01f  // 16:9只显示16:9分辨率，16:10只显示16:10分辨率
                       && r.width >= 1080
                       && r.height >= 720;
            })
            .OrderByDescending(r => r.width)
            .ThenByDescending(r => r.height)
            .ToArray();
    }

    void SetupDropdown()
    {
        resolutions.ClearOptions();
        PopulateResolutionOptions();
        
        resolutions.value = FindCurrentResolutionIndex();
        resolutions.RefreshShownValue();
        resolutions.onValueChanged.AddListener(HandleResolutionChange);
    }

    private int FindCurrentResolutionIndex()
    {
        // 获取当前屏幕分辨率
        Resolution current = new Resolution { 
            width = Screen.currentResolution.width, 
            height = Screen.currentResolution.height 
        };
        
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (availableResolutions[i].width == current.width && 
                availableResolutions[i].height == current.height)
            {
                return i;
            }
        }
        return 0;
    }

    void PopulateResolutionOptions()
    {
        var options = availableResolutions
            .Select(r => $"{r.width}x{r.height}")
            .ToList();
        
        resolutions.AddOptions(options);
    }

    void HandleResolutionChange(int selectedIndex)
    {
        var selected = availableResolutions[selectedIndex];
        Screen.SetResolution(selected.width, selected.height, FullScreenMode.FullScreenWindow);
    }
}