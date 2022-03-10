using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MineCardUI : MonoBehaviour
{
    public MineCard mineCard;
    public TextMeshProUGUI explorationText;
    public TextMeshProUGUI incomeText;

    public void Start()
    {
        UpdateCardContent();
    }

    public void UpdateCardContent()
    {
        SetExplorationText();
        SetIncomeText();
    }


    public void SetExplorationText()
    {
        explorationText.text = "Strate la plus profonde\n" + mineCard.progress;
    }

    public void SetIncomeText()
    {
        incomeText.text = mineCard.incomeParMinute + " $ / minute";
    }
}
