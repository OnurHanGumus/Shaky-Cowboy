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
    protected const int _MAKS_HEALTH = 100;

    #endregion

    #endregion

    #region Event Subscription
    protected virtual void OnEnable()
    {
        SubscribeEvents();
        _currentHealth = _MAKS_HEALTH;
    }
    private void SubscribeEvents()
    {

    }
    private void UnSubscribeEvents()
    {

    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    #endregion



    protected virtual void Awake()
    {
        Init();
    }
    private void Init()
    {
        _currentHealth = _MAKS_HEALTH;
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0, -playerTransform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void SetHealthBarScale(int currentValue, int maxValue)//HealthBar increase or decrease with this method. This method can also listen a signal.
    {
        healthBar.localScale = new Vector3((float)currentValue / maxValue, 1, 1);
    }
    public virtual void OnHitted(int value, StickmanBodyPartEnums bodyPart)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
        HealthText.text = _currentHealth.ToString();

        SetHealthBarScale(_currentHealth, 100);

        
    }

    //HEALTHBAR VISIBILITY
    private void OnPlayerReachToBase()
    {
        holder.SetActive(false);

    }
    private void OnPlayerLeaveTheBase()
    {
        holder.SetActive(true);
    }
}
