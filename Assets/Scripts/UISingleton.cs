using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISingleton : MonoBehaviour
{
    public static UISingleton uiSingleton { get; private set; }
    // public static UISingleton uiSingleton;

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
        if (uiSingleton != null && uiSingleton != this) {
            Destroy(this);
        } else {
            uiSingleton = this;
        }
    }

    void Start()
    {
      // infoBarContainer = GameObject.Find("InfoBar").GetComponent<Image>();
    }

    void Update()
    {
        
    }
}
