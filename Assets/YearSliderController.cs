using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YearSliderController : MonoBehaviour
{
    public Slider yearSlider;
    public DataPointPlotter dataPointPlotter;
    public TMP_Text yearText;
    public MonoBehaviour scriptToDisable;

    private bool isSliderBeingUsed = false;

    private void Start()
    {
        yearSlider.minValue = 1900;
        yearSlider.maxValue = 2020;
        yearSlider.value = dataPointPlotter.YearNumber;

        yearSlider.onValueChanged.AddListener(UpdateYearNumber);

        UpdateYearText(dataPointPlotter.YearNumber);
    }

    private void UpdateYearNumber(float value)
    {
        // Update-YearNumber
        dataPointPlotter.YearNumber = (int)value;

        
        UpdateYearText(dataPointPlotter.YearNumber);

        if (isSliderBeingUsed)
        {
            
            scriptToDisable.enabled = false;
        }
    }

    private void UpdateYearText(int year)
    {
        
        yearText.text = "Year: " + year.ToString();
    }

    public void OnSliderClick()
    {
        isSliderBeingUsed = true;
    }

    public void OnSliderRelease()
    {
        isSliderBeingUsed = false;

        
        scriptToDisable.enabled = true;
    }
}
