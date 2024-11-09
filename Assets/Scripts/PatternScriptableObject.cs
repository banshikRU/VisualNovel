using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "Pattern", menuName = "Patern", order = 51)]
public class PatternScriptableObject : ScriptableObject
{ 
    public bool isQuiz;
    public List<PatternScriptableObject> quizPatterns = new List<PatternScriptableObject>();
    public List<string> quizVariants = new List<string>();
    public List<string> dialogText;
    public List<string> names;
    public Sprite mainBackGround;
    public Sprite mainPerson;
    public Sprite mainPersonSecond;
    public Sprite dialogPerson;
    public Sprite firstPerson;
    public Sprite secondPerson;
    public Sprite endBackGround;
    public bool SmoothTransition;
    public bool NewSceneNewPerson;
    public bool mainBackGroundBiger;
    public bool isBrush;
    public bool isMaksChpok;
    public bool isManagerIncident;
    public float ScaleSpeed;
    public Vector3 Scale;
    public bool isAlinaGoHome;
    public bool maksVAhue;
    public bool isGGGoOut;
    public AudioClip sfx;
    public PatternScriptableObject nextPattern;
   
   
   
   
   
   
    
   
}
