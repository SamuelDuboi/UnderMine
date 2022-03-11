using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MacroManager : MonoBehaviour
{
    public List<MineCard> listMine;
    public List<MinerCard> listMiner;

    [Space(15)]

    public MineCard selectedMine;

    [Space(15)]

    public GameObject mineSelectionCanvas;
    public GameObject minerSelectionCanvas;
    public GameObject tradeMenuCanvas;

    [Space(15)]

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI ethText;

    [Space(15)]

    public List<GameObject> minePrefabs;
    public List<GameObject> minePrefabsMiner;
    public List<GameObject> minePrefabsTrading;
    public List<GameObject> minerPrefabs;
    public List<GameObject> minerPrefabsTrading;

    [Space(15)]

    public Button nextMineButton;
    public Button previousMineButton;
    public Button nextMinerButton;
    public Button previousMinerButton;
    public Button nextMineTradeButton;
    public Button previousMineTradeButton;
    public Button nextMinerTradeButton;
    public Button previousMinerTradeButton;
    public Button sellMineButton;
    public Button sellMinerButton;
    public Button buyMineButton;
    public Button buyMinerButton;

    [Space(15)]

    public TextMeshProUGUI sellMineText;
    public TextMeshProUGUI sellMinerText;


    private bool canGenerateIncome = true;

    private int indexMineSelection = 0;
    private int indexMinerSelection = 0;

    private bool isTrading;
    private void Start()
    {
        for (int i = 0; i < listMine.Count; i++)
        {
            var myMine = SaveSystem.Instance.mines[i];
            listMine[i].progress = myMine.strat;
            for (int x = 0; x < listMine[i].inventory.inventory.Count; x++)
            {
                listMine[i].inventory.inventory[myMine.cryptos[x]] = myMine.cryptosValue[x];
            }
           
        }
        RevaluateAllMineIncome();
        RevaluateMinerEthPrices();
        StartCoroutine(GenerateIncome());
        UpdateMineButtonsInteractivity();
        UpdateMineCardView();
        UpdateSellButton();
    }

    private void Update()
    {
        moneyText.text = (Mathf.Round(ValueManager.instance.globalMoney*100) / 100) + " $";
        ethText.text =  ValueManager.instance.CurrentEtherum + " ETH";
    }

    public void RevaluateAllMineIncome()
    {
        foreach(MineCard mc in listMine)
        {
            mc.RevaluateIncome();
        }
    }

    public void RevaluateMinerEthPrices()
    {
        foreach (MinerCard mc in listMiner)
        {
            mc.UpdateEthPrice();
        }
    }

    private IEnumerator GenerateIncome()
    {
        while(canGenerateIncome)
        {
            ValueManager.instance.AddGlobalMoney( CalculateIncomePerQuarterSecond());
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
        ValueManager.instance.NextMine(-1);
        UpdateMineButtonsInteractivity();
        UpdateMineCardView();
        UpdateSellButton();
    }

    public void NextMineAction()
    {
        indexMineSelection += 1;
        ValueManager.instance.NextMine(1);
        UpdateMineButtonsInteractivity();
        UpdateMineCardView();
        UpdateSellButton();
    }

    public void ConfirmMineAction()
    {
        selectedMine = listMine[indexMineSelection];
        mineSelectionCanvas.SetActive(false);
        foreach (var card in minePrefabs)
        {
            card.SetActive(false);
        }
        minePrefabsMiner[indexMineSelection].SetActive(true);
        minerSelectionCanvas.SetActive(true);
        minePrefabsMiner[indexMineSelection].GetComponentInChildren<MineCardUI>().mineCard = selectedMine;
        minePrefabsMiner[indexMineSelection].GetComponentInChildren<MineCardUI>().UpdateCardContent();
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
    }

    private void UpdateMineButtonsInteractivity()
    {
        if (indexMineSelection == 0)
        {
            previousMineButton.interactable = false;
            previousMineTradeButton.interactable = false;
            sellMineButton.interactable = false;
        }
        else
        {
            previousMineButton.interactable = true;
            previousMineTradeButton.interactable = true;
            sellMineButton.interactable = true;
        }


        if (indexMineSelection == listMine.Count - 1)
        {
            nextMineButton.interactable = false;
            nextMineTradeButton.interactable = false;
        }
        else
        {
            nextMineButton.interactable = true;
            nextMineTradeButton.interactable = true;
        }

        if(ValueManager.instance.CurrentEtherum < 0.01f)
        {
            buyMineButton.interactable = false;
        }
        else
        {
            buyMineButton.interactable = true;
        }
    }

    private void UpdateMineCardView()
    {
        foreach (var min in minePrefabs)
        {
            min.SetActive(false);
        }
        foreach (var min in minePrefabsTrading)
        {
            min.SetActive(false);
        }
        if (isTrading)
        {
            minePrefabsTrading[indexMineSelection].SetActive(true);
        }
        else
            minePrefabs[indexMineSelection].SetActive(true);

        minePrefabs[indexMineSelection].GetComponentInChildren<MineCardUI>().mineCard = listMine[indexMineSelection];
        minePrefabsTrading[indexMineSelection].GetComponentInChildren<MineCardUI>().mineCard = listMine[indexMineSelection];

        minePrefabs[indexMineSelection].GetComponentInChildren<MineCardUI>().UpdateCardContent();
        minePrefabsTrading[indexMineSelection].GetComponentInChildren<MineCardUI>().UpdateCardContent();
    }
    #endregion

    #region Miner Selection UI

    public void PreviousMinerAction()
    {
        indexMinerSelection -= 1;
        ValueManager.instance.NextMiner(-1);
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
        UpdateSellButton();
    }

    public void NextMinerAction()
    {
        indexMinerSelection += 1;
        ValueManager.instance.NextMiner(1);
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
        UpdateSellButton();
    }

    public void ConfirmMinerAction()
    {
        ValueManager.instance.SetMinerCard( listMiner[indexMinerSelection]);
        minerSelectionCanvas.SetActive(false);
        mineSelectionCanvas.SetActive(true);

        SceneManager.LoadScene(selectedMine.sceneName);
    }

    private void UpdateMinerButtonsInteractivity()
    {
        if (indexMinerSelection == 0)
        {
            previousMinerButton.interactable = false;
            previousMinerTradeButton.interactable = false;
            sellMinerButton.interactable = false;
        }
        else
        {
            previousMinerButton.interactable = true;
            previousMinerTradeButton.interactable = true;
            sellMinerButton.interactable = true;
        }


        if (indexMinerSelection == listMiner.Count - 1)
        {
            nextMinerButton.interactable = false;
            nextMinerTradeButton.interactable = false;
        }
        else
        {
            nextMinerButton.interactable = true; 
            nextMinerTradeButton.interactable = true;
        }

        if (ValueManager.instance.CurrentEtherum < 0.01f)
        {
            buyMinerButton.interactable = false;
        }
        else
        {
            buyMinerButton.interactable = true;
        }
    }

    private void UpdateMinerCardView()
    {
        foreach (var min in minerPrefabs)
        {
            min.SetActive(false);
        }
        foreach (var min in minerPrefabsTrading)
        {
            min.SetActive(false);
        }
        if (isTrading)
        {
            minerPrefabsTrading[indexMinerSelection].SetActive(true);
        }
        else
            minerPrefabs[indexMinerSelection].SetActive(true);
        minerPrefabs[indexMinerSelection].GetComponent<MinerCardUI>().minerCard = listMiner[indexMinerSelection];
        minerPrefabsTrading[indexMinerSelection].GetComponent<MinerCardUI>().minerCard = listMiner[indexMinerSelection];
        minerPrefabs[indexMinerSelection].GetComponent<MinerCardUI>().UpdateCardContent();
        minerPrefabsTrading[indexMinerSelection].GetComponent<MinerCardUI>().UpdateCardContent();
    }

    public void BackToMineAction()
    {
        selectedMine = null;
        minerSelectionCanvas.SetActive(false);
        foreach (var min in minerPrefabs)
        {
            min.SetActive(false);
        }
        foreach (var min in minePrefabsMiner)
        {
            min.SetActive(false);
        }
        minePrefabs[indexMineSelection].SetActive(true);
        mineSelectionCanvas.SetActive(true);
    }

    #endregion

    #region Trade Menu UI

    public void OpenTradeMenuAction()
    {
        mineSelectionCanvas.SetActive(false);
        foreach (var min in minePrefabs)
        {
            min.SetActive(false);
        }
        foreach (var min in minerPrefabs)
        {
            min.SetActive(false);
        }
        minePrefabsTrading[indexMineSelection].SetActive(true);
        isTrading = true;
        tradeMenuCanvas.SetActive(true);
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
        UpdateSellButton();
    }

    public void CloseTradeMenuAction()
    {
        foreach (var min in minePrefabsTrading)
        {
            min.SetActive(false);
        }
        foreach (var min in minerPrefabsTrading)
        {
            min.SetActive(false);
        }
        minePrefabs[indexMineSelection].SetActive(true);
        isTrading = false;
        tradeMenuCanvas.SetActive(false);
        mineSelectionCanvas.SetActive(true);
    }

    public void SellMineAction()
    {
        ValueManager.instance.AddCurrentEtherum(listMine[indexMineSelection].ethPrice);
        listMine.RemoveAt(indexMineSelection);
        
        if(indexMineSelection >= listMine.Count)
        {
            indexMineSelection = listMine.Count - 1;
        }

        UpdateMineButtonsInteractivity();
        UpdateMinerButtonsInteractivity();
        UpdateMineCardView();
        UpdateSellButton();
    }

    public void SellMinerAction()
    {
        ValueManager.instance.AddCurrentEtherum(listMiner[indexMinerSelection].ethPrice);
        listMiner.RemoveAt(indexMinerSelection);

        if (indexMinerSelection >= listMiner.Count)
        {
            indexMinerSelection = listMiner.Count - 1;
        }

        UpdateMineButtonsInteractivity();
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
        UpdateSellButton();
    }

    public void BuyMineAction()
    {
        ValueManager.instance.AddCurrentEtherum(-0.01f);
        MineCard newMineCard = ScriptableObject.CreateInstance<MineCard>();
        newMineCard.sceneName = "Mine" + Random.Range(1, 4);
        newMineCard.rarity = (Rarity)Random.Range(0, 4);
        newMineCard.biome = (Biome)Random.Range(0, 5);
        //AssetDatabase.CreateAsset(newMineCard, "Assets/MineCard/Mine" + Random.Range(5, 999999999) + ".asset"); // Inshallah
        listMine.Add(newMineCard);

        UpdateMineButtonsInteractivity();
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
        UpdateSellButton();
    }

    public void BuyMinerAction()
    {
        ValueManager.instance.AddCurrentEtherum(-0.01f);
        MinerCard newMinerCard = ScriptableObject.CreateInstance<MinerCard>();
        newMinerCard.rarity = (Rarity)Random.Range(0, 4);
        newMinerCard.biome = (Biome)Random.Range(0, 5);
        newMinerCard.movementSpeed = Random.Range(85,200)/100.0f;
        newMinerCard.miningSpeed = Random.Range(85, 200) / 100.0f;
        newMinerCard.buildingSpeed = Random.Range(5, 45);
        newMinerCard.buildingCost = Random.Range(15, 50);
        //AssetDatabase.CreateAsset(newMinerCard, "Assets/MinerCard/Miner" + Random.Range(1, 999999999) + ".asset"); // Ouai ouai Inshallah encore
        listMiner.Add(newMinerCard);

        UpdateMineButtonsInteractivity();
        UpdateMinerButtonsInteractivity();
        UpdateMinerCardView();
        UpdateSellButton();
    }

    public void UpdateSellButton()
    {
        sellMineText.text = "Vendre : +" + listMine[indexMineSelection].ethPrice + " ETH";
        sellMinerText.text = "Vendre : +" + listMiner[indexMinerSelection].ethPrice + " ETH";
    }

    #endregion

    public void QuitGameAction()
    {
        Application.Quit();
    }
}