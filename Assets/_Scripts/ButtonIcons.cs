using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIcons : MonoBehaviour
{
    [SerializeField] private Button[] lvlButton;
    [SerializeField] private Sprite unlockedIcon;
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private int firstLevelBuildIndex;

    private void Awake()
    {
        var unlockedLvl = PlayerPrefs.GetInt(EndGameManager.Instance.lvlUnlock, firstLevelBuildIndex);
        for (var i = 0; i < lvlButton.Length; i++)
            if (i + firstLevelBuildIndex <= unlockedLvl)
            {
                lvlButton[i].interactable = true;
                lvlButton[i].image.sprite = unlockedIcon;

                var textButton = lvlButton[i].GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = (i + 1).ToString();
                textButton.enabled = true;
            }
            else
            {
                lvlButton[i].interactable = false;
                lvlButton[i].image.sprite = lockedIcon;

                var textButton =
                    lvlButton[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textButton != null)
                    textButton.enabled = false;
            }
    }
}