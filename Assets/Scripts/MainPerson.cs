using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainPerson : MonoBehaviour
{
    private Image _image;
    public static MainPerson instance;
    private void Awake()
    {
        instance = this;
        _image = GetComponent<Image>();
    }

}
