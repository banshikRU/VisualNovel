using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityAction firstButtonAction;
    public UnityAction secondButtonAction;
    public UnityAction thirdButtonAction;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _buttonNext;
    [SerializeField] private GameObject _buttonRestart;

    [SerializeField] private GameObject _textBox;
    [SerializeField] private GameObject _quizBox;
    [SerializeField] private GameObject _brush;
    [SerializeField] private GameObject _person1;
    [SerializeField] private GameObject _person2;
    [SerializeField] private GameObject _person3;
    [SerializeField] private GameObject _maksVPlane;
    [SerializeField] private GameObject _playerInput;
    [SerializeField] private GameObject _GGGoHome;


    [SerializeField] private Image _person1Image;
    [SerializeField] private Image _person2Image;
    [SerializeField] private Image _mainBackGroundImage;
    [SerializeField] private Image _mainPerson;
    [SerializeField] private Image _mainPersonSecond;
    [SerializeField] private Image _dialogPerson;
    [SerializeField] private Sprite _maksChpok1;
    [SerializeField] private Sprite _maksChpok2;

    [SerializeField] private List<string> _baseText;

    [SerializeField] private PatternScriptableObject _currentPattern;
    private PatternScriptableObject nextPattern;
    private int _curentPuternIndex;

    private Animator _person1Anim;
    private Animator _person2Anim;
    private Animator _person3Anim;

    private string _playerName;
    private bool _isFirstLevelInit;
    private bool _isSecondLevelInit;
    private bool _nameSeted;
    private bool _doFade;

    private bool _isMaksChpokMoment;
    private bool _isBigger;
    [SerializeField] private float _isBiggerTimer;
    private float _t;

    private List<string> curentQueueText = new List<string>();
    private List<string> curentQueueName = new List<string>();

    private int _textIterator;
    private void Awake()
    {
        firstButtonAction += SetUpFirstVariant;
        secondButtonAction += SetUpSecondVariant;
        thirdButtonAction += SetUpThirdVariant;
        Instance = this;
        _person3Anim = _person3.GetComponent<Animator>();
        _person1Anim = _person1.GetComponent<Animator>();
        _person2Anim = _person2.GetComponent<Animator>();
    }
    public void RestartScene()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        AudioManager.instance.PlayMusic();
        _mainMenu.SetActive(false);
        _gameMenu.SetActive(true);
        TextIterator();
        _person1.SetActive(true);
        _person1Anim.Play("Person1");
        _buttonRestart.SetActive(false);
        _buttonNext.SetActive(true);
    }
    private void Start()
    {
        
        _nameSeted = false;
        _t = _isBiggerTimer;
        _isFirstLevelInit = true;
        _isSecondLevelInit = true;
        _curentPuternIndex = 0;
        curentQueueText.AddRange(_baseText);
        _textIterator = 0;
    }
    public void TakePlayerInput()
    {
        if (_doFade) { return; }
        if (_isMaksChpokMoment) { return; }
        if (_quizBox.activeSelf) { return; }
        if (_playerInput.activeSelf == true && _nameSeted == false ) { return; }
        TextIterator();
    }
    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.V) && _isMaksChpokMoment== false) { TextIterator(); }
        //if (_isBigger)
        //{
        //    _t -= Time.fixedDeltaTime;
        //    if (_t > 0)
        //    {
        //        Mathf.Lerp(_mainBackGroundImage.rectTransform.sca)
        //    }
        //}
    }
    private IEnumerator MaksChpok()
    {
        _isMaksChpokMoment = true;
        _mainBackGroundImage.sprite = _maksChpok1;
        yield return new WaitForSeconds(0.5f);
        _mainBackGroundImage.sprite = _maksChpok2; 
        yield return new WaitForEndOfFrame();
        _isMaksChpokMoment=false;
    }
   
    private void TextIterator()
    {
        ClearPlayerName();
        if (curentQueueText[_textIterator] == "-")
        {
            AudioManager.instance.EndMusic();
            ClearAllOldPatternInfo();
            _mainBackGroundImage.sprite = _currentPattern.endBackGround;
            _textBox.SetActive(false);
            _buttonRestart.SetActive(false);
            _buttonRestart.SetActive(true);
        }
        else
        {
                if (curentQueueText[_textIterator] == ".")
                {
                    ParsePatern();
                }

                if (_textIterator == 1 && _isFirstLevelInit)
                {
                   // _isFirstLevelInit = false;
                   // TakePlayerName();
                }
                if (_textIterator == 2 && _isSecondLevelInit) { SetPlayerName(); _isSecondLevelInit = false; }
                if (curentQueueText[_textIterator].Contains("[name]"))
                {
                    curentQueueText[_textIterator] = curentQueueText[_textIterator].Replace("[name]", _playerName);
                }
                NamesTextUpdate();
                PersonTextManager.instance.UpdatePersonText(curentQueueText[_textIterator]);
                _textIterator++;
            
        }
    }
    private void ClearPlayerName()
    {
        PersonNameManager.instance.UpdatePersonName("");
    }
    private void SetUpFirstVariant()
    {
        int a = 0;
        if (a>= 0 && a< _currentPattern.quizPatterns.Count)
        {
            _currentPattern.nextPattern = _currentPattern.quizPatterns[a];
            ParsePatern();
        }
    }
    private void SetUpSecondVariant()
    {
        int c = 1;
        if (c >= 0 && c < _currentPattern.quizPatterns.Count)
        {
            _currentPattern.nextPattern = _currentPattern.quizPatterns[c];
            ParsePatern();
        }
    }
    private void SetUpThirdVariant()
    {
        int b = 2;
        if (b >= 0 && b < _currentPattern.quizPatterns.Count)
        {
            _currentPattern.nextPattern = _currentPattern.quizPatterns[b];
            ParsePatern();
        }
    }
    private void TakePlayerName()
    {
        _playerInput.SetActive(true);
    }
    public void SetPlayerName()
    {
        _playerName = _playerInput.GetComponent<TMPro.TMP_InputField>().text;
        _playerInput.SetActive(false);
        _nameSeted = true;
    }
    private void ParsePatern()
    {
        ClearAllOldPatternInfo();
        nextPattern = _currentPattern.nextPattern;
        _currentPattern = nextPattern;
        if (nextPattern.isQuiz)
        {

            _textBox.SetActive(false);
            _quizBox.SetActive(true);
            QuizBoxManager.instance.UpdateQuizBoxText(nextPattern.quizVariants);
        }
        else
        {
            CheckForSfx();
            _textBox.SetActive(true);
            _quizBox.SetActive(false);
            ClearAllQueue();
            UpdateAllCurentQueue(nextPattern);
            _curentPuternIndex++;
            CheckForSmoothTransition();
            CheckForBrush();
            CheckForMaksChpok();
             CheckForGroundBiger();
            IfAlinaGoHome();
            CheckIfMaksVAhue();
            CheckIfGGGOHome();
            _textIterator = 0;
            TextIterator();
        }  
    }
    private void CheckForSfx()
    {
        if (_currentPattern.sfx != null) { AudioManager.instance.PlaySingle(_currentPattern.sfx); }
    }
    private void CheckIfGGGOHome()
    {
        if (_currentPattern.isGGGoOut)
        {
            _GGGoHome.SetActive(true);
            _GGGoHome.GetComponent<Animator>().Play("GGGoHome");
        }
    }
    private void CheckIfMaksVAhue()
    {
        if (_currentPattern.maksVAhue)
        {
            _maksVPlane.SetActive(true);
            _maksVPlane.GetComponent<Animator>().Play("MaksVplane");
        }
    }
    private void IfAlinaGoHome()
    {
        if (_currentPattern.isAlinaGoHome)
        {
            _person3.SetActive(true);
        }
    }
    private void CheckForMaksChpok()
    {
        if (_currentPattern.isMaksChpok)
        {
            StartCoroutine(MaksChpok());
        }
    }
    private void CheckForBrush()
    {
        if (_currentPattern.isBrush)
        {
            _brush.SetActive(true);
        }
    }
    private void ClearAllOldPatternInfo()
    {
        _GGGoHome.SetActive(false);
        _maksVPlane.SetActive(false);
        _mainBackGroundImage.rectTransform.DOScale(new Vector3(1, 1, 1), 0);
        _textBox.SetActive(false);
        _person1.SetActive(false );
        _person2.SetActive(false );
        _mainPerson.gameObject.SetActive(false );
        _mainPersonSecond.gameObject.SetActive(false );
        _dialogPerson.gameObject.SetActive(false );
        _brush.SetActive(false);
    }
    private void CheckForGroundBiger()
    {
        if (_currentPattern.mainBackGroundBiger)
        {
            _mainBackGroundImage.rectTransform.DOScale(_currentPattern.Scale, _currentPattern.ScaleSpeed);
        }
    }
    private IEnumerator SmoothTransition(List<Image> transitionImages)
    {
        _doFade = true;
        foreach (Image image in transitionImages)
        {
            image.DOFade(0, 1f);
        }
        yield return new WaitForSeconds(1.1f);
        ReskinCharacter();
        foreach (Image image in transitionImages)
        {
            image.DOFade(1, 1f);
        }
        yield return new WaitForSeconds(1.2f);
        _doFade = false;
        yield return new WaitForEndOfFrame();
    }
    private void CheckForSmoothTransition()
    { 

           StartCoroutine( SmoothTransition(new List<Image>() { _mainBackGroundImage, _mainPerson, _mainPersonSecond }));

    }
    private List<Image> AllImagesNeedReskin()
    {
        List<Image> result = new List<Image>();
        if (!_mainPerson.sprite == nextPattern.firstPerson) { result.Add(_mainPerson); }
        if (!_mainPersonSecond.sprite == nextPattern.secondPerson) { result.Add(_mainPersonSecond); }
        if (!_mainBackGroundImage.sprite == nextPattern.mainBackGround) { result.Add(_mainBackGroundImage); }
        if (!_dialogPerson.sprite == nextPattern.dialogPerson) { result.Add(_dialogPerson); }
        return result;
    }
    private void ReskinCharacter()
    {
        _person1Image.sprite = nextPattern.firstPerson;
        _person2Image.sprite = nextPattern.secondPerson;
        _mainPerson.sprite = nextPattern.mainPerson;
        _mainBackGroundImage.sprite = nextPattern.mainBackGround;
        _mainPersonSecond.sprite = nextPattern.mainPersonSecond;
        _dialogPerson.sprite = nextPattern.dialogPerson;
        if (_mainPerson.sprite!= null)
        {
            _mainPerson.gameObject.SetActive(true);
        }
        if (_mainPersonSecond.sprite!= null)
        {
            _mainPersonSecond.gameObject.SetActive(true);
        }
        if (nextPattern.dialogPerson!= null)
        {
            _dialogPerson.gameObject.SetActive(true);
        }
        if (_person1Image.sprite != null)
        {
            _person1.SetActive(true);
            _person1Anim.Play("Person1");
        }
        if (_person2Image.sprite != null)
        {
            _person2.SetActive(true);
            _person1Anim.Play("Person2");
        }

    }
    private void ClearAllQueue()
    {
        curentQueueText.Clear();
        curentQueueName.Clear();
    }
    private void UpdateAllCurentQueue(PatternScriptableObject curentPattern)
    {
        curentQueueName = new List<string>(curentPattern.names);
        curentQueueText = new List<string>(curentPattern.dialogText);
    }
    private void NamesTextUpdate()
    {
       if (curentQueueName.Count!= 0)
       {
            if (_textIterator < curentQueueName.Count & _textIterator>=0)
            {
                if (curentQueueName[_textIterator] == "GG")
                {
                    PersonNameManager.instance.UpdatePersonName("Настя");
                }
                else
                {
                    PersonNameManager.instance.UpdatePersonName(curentQueueName[_textIterator]);
                }
            }
       }
    }

}

