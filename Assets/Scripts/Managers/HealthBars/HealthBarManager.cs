using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;
using Zenject;
using Enums;

public class HealthBarManager : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion

    #region Public Variables
    public TextMeshPro HealthText;

    #endregion

    #region Serialized Variables
    [SerializeField] protected Transform playerTransform;
    [SerializeField] private Transform healthBar;

    [SerializeField] private GameObject holder;

    #endregion

    #region Private Variables
    protected int _currentHealth = 100;
    protected int _maksHealth = 0;

    #endregion

    #endregion

    #region Event Subscription
    protected virtual void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestart;
    }

    protected virtual void UnSubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestart;

    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    #endregion

    public void InitHealthValue(int maksHealth)
    {
        _maksHealth = maksHealth;
        _currentHealth = maksHealth;
        HealthText.text = maksHealth.ToString();
    }

    //private void Update()
    //{
    //    transform.localEulerAngles = new Vector3(0, -playerTransform.eulerAngles.y, 0);
    //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //}

    public void SetHealthBarScale(int currentValue, int maxValue)//HealthBar increase or decrease with this method. This method can also listen a signal.
    {
        healthBar.localScale = new Vector3((float)currentValue / maxValue, 1, 1);
    }

    public void ChangeHealthbar(int value)
    {
        _currentHealth = value;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
        HealthText.text = _currentHealth.ToString();

        SetHealthBarScale(_currentHealth, _maksHealth);
    }

    //HEALTHBAR VISIBILITY
    protected virtual void OnRestart()
    {
        _currentHealth = _maksHealth;
        HealthText.text = _currentHealth.ToString();
        SetHealthBarScale(_currentHealth, _maksHealth);
    }
}
