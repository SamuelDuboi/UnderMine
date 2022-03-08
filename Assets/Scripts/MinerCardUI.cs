using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinerCardUI : MonoBehaviour
{
    public MinerCard minerCard;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI biomeText;
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
        SetRarityText();
        SetBiomeText();
        SetMovementSpeedText();
        SetMiningSpeedText();
        SetBuildingSpeedText();
        SetBuildingCostText();
    }

    private void SetRarityText()
    {
        switch(minerCard.rarity)
        {
            case Rarity.COMMON:
                rarityText.text = "Commune";
                break;

            case Rarity.RARE:
                rarityText.text = "Rare";
                break;

            case Rarity.EPIC:
                rarityText.text = "Epique";
                break;

            case Rarity.LEGENDARY:
                rarityText.text = "Légendaire";
                break;
        }
    }

    public void SetBiomeText()
    {
        switch (minerCard.biome)
        {
            case Biome.COBBLESTONE:
                biomeText.text = "Standard";
                break;

            case Biome.SANDSTONE:
                biomeText.text = "Sable";
                break;

            case Biome.ICE:
                biomeText.text = "Glace";
                break;

            case Biome.MAGMA:
                biomeText.text = "Magma";
                break;

            case Biome.CRYSTAL:
                biomeText.text = "Cristal";
                break;
        }
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
