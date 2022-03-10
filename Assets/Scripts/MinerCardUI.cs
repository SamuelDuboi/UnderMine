using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinerCardUI : MonoBehaviour
{
    public MinerCard minerCard;
    public TextMeshProUGUI movementSpeedText;
    public TextMeshProUGUI miningSpeedText;
    public TextMeshProUGUI buildingSpeedText;
    public TextMeshProUGUI buildingCostText;

    public void Start()
    {
        UpdateCardContent();
    }

    public void UpdateCardContent()
    {
        SetMovementSpeedText();
        SetMiningSpeedText();
        SetBuildingSpeedText();
        SetBuildingCostText();
    }


    public void SetMovementSpeedText()
    {
        movementSpeedText.text = "Vitesse de déplacement\n" + minerCard.movementSpeed*100 + "%";
    }

    public void SetMiningSpeedText()
    {
        miningSpeedText.text = "Vitesse de minage\n" + minerCard.miningSpeed * 100 + "%";
    }

    public void SetBuildingSpeedText()
    {
        buildingSpeedText.text = "Temps de construction\n" + minerCard.buildingSpeed + " sec.";
    }

    public void SetBuildingCostText()
    {
        buildingCostText.text = "Cout de la foreuse\n" + minerCard.buildingCost + " $";
    }
}
