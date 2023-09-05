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
        // Update the YearNumber variable in the DataPointPlotter script
        dataPointPlotter.YearNumber = (int)value;

        // Update the TMP Text component with the new YearNumber
        UpdateYearText(dataPointPlotter.YearNumber);

        if (isSliderBeingUsed)
        {
            // Disable the script when using the slider
            scriptToDisable.enabled = false;
        }
    }

    private void UpdateYearText(int year)
    {
        // Update the TMP Text component with the current YearNumber
        yearText.text = "Year: " + year.ToString();
    }

    public void OnSliderClick()
    {
        isSliderBeingUsed = true;
    }

    public void OnSliderRelease()
    {
        isSliderBeingUsed = false;

        // Re-enable the script when not using the slider
        scriptToDisable.enabled = true;
    }
}
