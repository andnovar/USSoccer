using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushToRadarChart : MonoBehaviour
{
    public GameObject Radar_Chart_Object;
    [SerializeField]
    float[] parameters = new float[6];
    [SerializeField]
    int[] parameters_int = new int[6];


    [SerializeField]
    int[] parameters_star;
    [SerializeField]
    float[] parameters_star_float = new float[6];

    public GameObject Message_Parser_Object;

    Component[] _childsliders;

    // Use this for initialization
    void Start()
    {
        _childsliders = gameObject.GetComponentsInChildren<Slider>();
        // setRadarChart();
    }

    public IEnumerable<int> GetParameters()
    {
        return parameters_int;
    }


    void setRadarChart()
    {
        if (Radar_Chart_Object) {
            if (_childsliders != null && _childsliders.Length > 0) {
                int i = 0;
                foreach (Slider slider in _childsliders) {
                    //Text text = gameObject.GetComponent(typeof(Text)) as Text;
                    Debug.Log(slider.name);

                    parameters_int[i] = (int) slider.value;
                    parameters[i++] = (slider.value / 100.0f);
                    if (i >= parameters.Length) break;
                }
            }

            GameObject obj = Radar_Chart_Object.transform.Find("After").gameObject;
            if (obj != null) {
                RadarChart radar = obj.GetComponent(typeof(RadarChart)) as RadarChart;
                radar.SetParameters(parameters);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        setRadarChart();
        sendRadarChart();
    }

    public void sendRadarChart()
    {
        if (Message_Parser_Object) {
            NetworkService networkService = Message_Parser_Object.GetComponent(typeof(NetworkService)) as NetworkService;
            networkService.SetParameters(parameters_int);
        }
    
    }

    public void setRadarChartStar()
    {
        if (Message_Parser_Object && Radar_Chart_Object) {

            MessageParser parser = Message_Parser_Object.GetComponent(typeof(MessageParser)) as MessageParser;

            //do {
            //    //parameters_star = (int[])parser.GetParameters();
            //    /*for (int i = 0; i < 6; ++i) {
            //        parameters_star_float[i] = parameters_star[i] / 100.0f;
            //    }*/
            //} while (!parser.OWNFLAG);

            parameters_star = (int[])parser.GetParameters();
            for (int i = 0; i < 6; ++i) {
                parameters_star_float[i] = parameters_star[i] / 100.0f;
            }

            GameObject obj = Radar_Chart_Object.transform.Find("Current").gameObject;
            if (obj != null) {
                RadarChart radar = obj.GetComponent(typeof(RadarChart)) as RadarChart;
                radar.SetParameters(parameters_star_float);
            }
        }
    }


}
