using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizBoxManager : MonoBehaviour
{
    public static QuizBoxManager instance;
    [SerializeField] private TextMeshProUGUI _firstVariant;
    [SerializeField] private TextMeshProUGUI _secondVariant;
    [SerializeField] private TextMeshProUGUI _thirdVariant;
    [SerializeField] private Button _firstVariantButton;
    [SerializeField] private Button _secondVariantButton;
    [SerializeField] private Button _thirdVariantButton;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateQuizBoxText(List<string> quizBoxText)
    {
        _firstVariantButton.onClick.AddListener(GameManager.Instance.firstButtonAction);
        _secondVariantButton.onClick.AddListener(GameManager.Instance.secondButtonAction);
        _thirdVariantButton.onClick.AddListener(GameManager.Instance.thirdButtonAction);
        _firstVariant.text = quizBoxText[0];
        _secondVariant.text = quizBoxText[1];
        _thirdVariant.text = quizBoxText[2];
    }
}
