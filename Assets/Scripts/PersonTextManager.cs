using TMPro;
using UnityEngine;

public class PersonTextManager : MonoBehaviour
{
    public static PersonTextManager instance;
    private TextMeshProUGUI _personText;
    private TextMeshProUGUI _personName;
    void Start()
    {
        _personText = GetComponent<TextMeshProUGUI>();
        instance = this;
    }
    public void UpdatePersonText(string text)
    {
        _personText.text = text;
    }
}
