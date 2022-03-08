using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MacroManager : MonoBehaviour
{
    public List<MineCard> listMine;
    public List<MinerCard> listMiner;

    public MineCard selectedMine;
    public MinerCard selectedMiner;

    public GameObject mineSelectionCanvas;
    public GameObject minerSelectionCanvas;
    public Text moneyText;
    public MineCardUI mineImage;
    public MineCardUI mineSelectedImage;
    public MinerCardUI minerImage;
    public Button nextMineButton;
    public Button previousMineButton;
    public Button nextMinerButton;
    public Button previousMinerButton;

    public float money = 0;
    private bool canGenerateIncome = true;

    private int indexMineSelection = 0;
    private int indexMinerSelection = 0;

    private void Start()
    {
        StartCoroutine(GenerateIncome());

        UpdateMineButtonsInteractivity();
        UpdateMineCardView();
    }

    private void Update()
    {
        moneyText.text = (Mathf.Round(money*100) / 100) + " $";
    }

    private IEnumerator GenerateIncome()
    {
        while(canGenerateIncome)
        {
            money += CalculateIncomePerQuarterSecond();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private float CalculateIncomePerQuarterSecond()
    {
        float totalIncomePerMinute = 0.0f;
        foreach(MineCard mc in listMine)
        {
            totalIncomePerMinute += mc.incomeParMinute;
        }
        return totalIncomePerMinute/240;
    }

    #region Mine Selection UI
    public void PreviousMineAction()
    {
        indexMineSelection -= 1;
        UpdateMineButtonsInteractivity();
        UpdateMineCardView();
    }

    public void NextMineAction()
    {
        indexMineSelection += 1;
        UpdateMineButtonsInteractivity();
        UpdateMineCardView();
    }

    public void ConfirmMineAction()
    {
        selectedMine = listMine[indexMineSelection];
        mineSelectionCanvas.SetActive(false);
        minerSelectionCanvas.SetActive(true);
        mineSelectedImage.mineCard = selectedMine;
        mineSelectedImage.UpdateCardContent();
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
    }

    private void UpdateMineButtonsInteractivity()
    {
        if (indexMineSelection == 0)
        {
            previousMineButton.interactable = false;
        }
        else
        {
            previousMineButton.interactable = true;
        }


        if (indexMineSelection == listMine.Count - 1)
        {
            nextMineButton.interactable = false;
        }
        else
        {
            nextMineButton.interactable = true;
        }
    }

    private void UpdateMineCardView()
    {
        mineImage.mineCard = listMine[indexMineSelection];
        mineImage.UpdateCardContent();
    }
    #endregion

    #region Miner Selection UI

    public void PreviousMinerAction()
    {
        indexMinerSelection -= 1;
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
    }

    public void NextMinerAction()
    {
        indexMinerSelection += 1;
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
    }

    public void ConfirmMinerAction()
    {
        selectedMiner = listMiner[indexMinerSelection];
        minerSelectionCanvas.SetActive(false);
        mineSelectionCanvas.SetActive(true);

        SceneManager.LoadScene(selectedMine.sceneName);
    }

    private void UpdateMinerButtonsInteractivity()
    {
        if (indexMinerSelection == 0)
        {
            previousMinerButton.interactable = false;
        }
        else
        {
            previousMinerButton.interactable = true;
        }


        if (indexMinerSelection == listMiner.Count - 1)
        {
            nextMinerButton.interactable = false;
        }
        else
        {
            nextMinerButton.interactable = true;
        }
    }

    private void UpdateMinerCardView()
    {
        minerImage.minerCard = listMiner[indexMinerSelection];
        minerImage.UpdateCardContent();
    }

    public void BackToMineAction()
    {
        selectedMine = null;
        minerSelectionCanvas.SetActive(false);
        mineSelectionCanvas.SetActive(true);
    }

    #endregion
}