using UnityEngine;
using TMPro;
using System.Linq;

public class DropdownResolution : MonoBehaviour
{
    public TMP_Dropdown resolutions;
    
    private Resolution[] availableResolutions;
    
    void Start()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        
        availableResolutions = Screen.resolutions
            .Select(r => new Resolution { width = r.width, height = r.height })
            .Distinct()
            .OrderByDescending(r => r.width)
            .ThenByDescending(r => r.height)
            .ToArray();
        
        resolutions.ClearOptions();
        PopulateResolutionOptions();
        
        resolutions.value = FindCurrentResolutionIndex();
        resolutions.RefreshShownValue();
        resolutions.onValueChanged.AddListener(HandleResolutionChange);
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

    int FindCurrentResolutionIndex()
    {
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (availableResolutions[i].width == Screen.width && 
                availableResolutions[i].height == Screen.height)
            {
                return i;
            }
        }
        return 0;
    }
}