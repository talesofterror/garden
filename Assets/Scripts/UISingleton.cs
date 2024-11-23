using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISingleton : MonoBehaviour
{
    private static UISingleton _uiSingleton;
    public static UISingleton uiSingleton { get {return _uiSingleton;} }

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI iconText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI charText;
    public TextMeshProUGUI infoBarText;
    public Image dialogueContainer;
    public Image iconContainer;
    public Image targetContainer;
    public Image inventoryContainer;
    public Image charContainer;
    public Image infoBarContainer;
    public DialogueManager diaglogueManager;

    void Awake () {
        if (_uiSingleton != null && _uiSingleton != this) {
            Destroy(this.gameObject);
        } else {
            _uiSingleton = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
      // infoBarContainer = GameObject.Find("InfoBar").GetComponent<Image>();
      // uiSingleton = new UISingleton();
    }

    void Update()
    {
        
    }
}
