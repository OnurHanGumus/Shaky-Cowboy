using System;
using UnityEngine;
using Zenject;
using TMPro;

public class MoneyPanelController : MonoBehaviour
{
    [Inject] private LoadGameDataCommand _loadCommand { get; set; }
    [Inject] private SaveGameCommand _saveCommand { get; set; }
    [Inject] private ScoreSignals _scoreSignals { get; set; }
    [SerializeField] TextMeshProUGUI moneyText;
    private int _currentMoney = 0;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        UpdateText();
    }

    private void Init()
    {
        SubscribeEvents();
        _currentMoney = _loadCommand.OnLoadGameData<int>(SaveDataEnums.Money);
    }

    private void SubscribeEvents()
    {
        _scoreSignals.onAmountChanged += ChangeAmount;
        _scoreSignals.onGetMoney += GetMoneyAmount;
    }

    private void UpdateText()
    {
        moneyText.text = _currentMoney.ToString();
    }

    private  void ChangeAmount(int value)
    {
        _currentMoney += value;
        _saveCommand.OnSaveData<int>(SaveDataEnums.Money, _currentMoney);
        UpdateText();
    }

    private int GetMoneyAmount()
    {
        return _currentMoney;
    }
}
