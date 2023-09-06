using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPointPlotter : MonoBehaviour
{
    public Transform globe; 
    public GameObject dataPointPrefab; 
    public TextAsset csvFile; 

    //colours 

    public Color minColor;
    public Color midColor;
    public Color maxColor;

    [Range(-11f, 6f)]
    public float minTempAnm = 0.34f;
    [Range(-11f, 6f)]
    public float midTempAnm = 0.24f;
    [Range(-11f, 6f)]
    public float highTempAnm = 2.15f;


    //year
    [Range(1900, 2022)]
    public int YearNumber;

    private List<CSVDataPoint> dataPoints = new List<CSVDataPoint>();
    private float currentYear = 1900;
    private float minXPosition = -10.0f; 
    private float maxXPosition = 10.0f;  

    [System.Serializable]
    public class CSVDataPoint
    {
        public int Year;
        public float Latitude;
        public float Longitude;
        public float TemperatureAnomaly;

        public CSVDataPoint(int year, float latitude, float longitude, float temperatureAnomaly)
        {
            Year = year;
            Latitude = latitude;
            Longitude = longitude;
            TemperatureAnomaly = temperatureAnomaly;
        }
    }

    void Start()
    {
        ParseCSV();
        PlotDataPoints();
    }

    void Update()
    {
        
        ChangeYearBasedOnXPosition(globe);
    }

    void ParseCSV()
    {
        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            if (values.Length >= 4)
            {
                if (int.TryParse(values[0], out int year) &&
                    float.TryParse(values[1], out float latitude) &&
                    float.TryParse(values[2], out float longitude) &&
                    float.TryParse(values[3], out float temperatureAnomaly))
                {
                    
                    if ((latitude >= 0) || (latitude < 0))
                    {
                        CSVDataPoint dataPoint = new CSVDataPoint(year, latitude, longitude, temperatureAnomaly);
                        dataPoints.Add(dataPoint);
                    }
                }
                else
                {
                    Debug.LogWarning("Skipping invalid data point: " + line);
                }
            }
        }
    }

    void PlotDataPoints()
    {
        foreach (CSVDataPoint dataPoint in dataPoints)
        {
            if (dataPoint.Year == Mathf.RoundToInt(currentYear)) 
            {
                Vector3 position = CalculatePositionOnGlobe(dataPoint.Latitude, dataPoint.Longitude);
                GameObject dataPointObj = Instantiate(dataPointPrefab, position, Quaternion.identity);
                Color color = CalculateColor(dataPoint.TemperatureAnomaly);
                dataPointObj.GetComponent<Renderer>().material.color = color;
            }
        }
    }

    Vector3 CalculatePositionOnGlobe(float latitude, float longitude)
    {
        
        float radius = 10f; 
        float latRad = latitude * Mathf.Deg2Rad;
        float lonRad = longitude * Mathf.Deg2Rad;

        float x = radius * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
        float y = radius * Mathf.Sin(latRad);
        float z = radius * Mathf.Cos(latRad) * Mathf.Sin(lonRad);

        return new Vector3(x, y, z);
    }

    Color CalculateColor(float temperatureAnomaly)
    {


        if (temperatureAnomaly <= minTempAnm)
        {
            return minColor; 
        }
        else if (temperatureAnomaly >= highTempAnm)
        {
            return maxColor; 
        }
        else if (temperatureAnomaly < midTempAnm)
        {
            
            float t = Mathf.InverseLerp(highTempAnm, midTempAnm, temperatureAnomaly);
            return Color.Lerp(minColor, minColor, t);
        }
        else
        {
            
            float t = Mathf.InverseLerp(midTempAnm, highTempAnm, temperatureAnomaly);
            return Color.Lerp(midColor, maxColor, t);
        }
    }





    void ChangeYearBasedOnXPosition(Transform objectToUse)
    {
        
        currentYear = YearNumber;
               
        DestroyDataPoints();
            
        PlotDataPoints();
    }

    void DestroyDataPoints()
    {
        GameObject[] dataPointObjs = GameObject.FindGameObjectsWithTag("DataPoint");
        foreach (GameObject obj in dataPointObjs)
        {
            Destroy(obj);
        }
    }
}
