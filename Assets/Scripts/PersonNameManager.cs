using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonNameManager : MonoBehaviour
{
    public static PersonNameManager instance;
    private TextMeshProUGUI _personName;
    private void Awake()
    {
        instance = this;
        _personName = GetComponent<TextMeshProUGUI>();
    }
    public void UpdatePersonName(string name)
    {
       _personName.text =name;
    }
}
