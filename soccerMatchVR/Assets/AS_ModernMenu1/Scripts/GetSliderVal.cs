using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSliderVal : MonoBehaviour
{
    public GameObject Parent_Slider;

    protected Slider _slider;

    protected Text _text;

    protected void getSliderValue()
    {
        if (_slider) {
            _text = gameObject.GetComponent(typeof(Text)) as Text;
            _text.text = _slider.value.ToString();
        }
    }

    void Start()
    {
        Parent_Slider = transform.parent.gameObject;

        if (Parent_Slider) {
            _slider = Parent_Slider.GetComponent(typeof(Slider)) as Slider;

            getSliderValue();
        }

    }
 
    void Update()
    {
        getSliderValue();
    }
}
