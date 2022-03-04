using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MineCardUI : MonoBehaviour
{
    public MineCard mineCard;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI biomeText;
    public TextMeshProUGUI explorationText;
    public TextMeshProUGUI prospectionText;
    public TextMeshProUGUI constructionText;
    public TextMeshProUGUI incomeText;

    private void Start()
    {
        SetRarityText();
        SetBiomeText();
        SetExplorationText();
        SetProspectionText();
        SetConstructionText();
        SetIncomeText();
    }

    private void SetRarityText()
    {
        switch (mineCard.rarity)
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
        switch(mineCard.biome)
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

    public void SetExplorationText()
    {
        explorationText.text = "Strate la plus profonde\n" + mineCard.progress;
    }

    public void SetProspectionText()
    {
        prospectionText.text = "Prospection\n" + (int)Mathf.Round(mineCard.prospect * 100) + " %";
    }

    public void SetConstructionText()
    {
        constructionText.text = "Construction\n" + (int)Mathf.Round(mineCard.construct * 100) + " %";
    }

    public void SetIncomeText()
    {
        incomeText.text = mineCard.incomeParMinute + " $ / minute";
    }
}
