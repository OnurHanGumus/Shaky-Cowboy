using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Signals;
using Zenject;

public class HealthBarManager : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    #endregion

    #region Public Variables
    public TextMeshPro HealthText;

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform healthBar;

    [SerializeField] private GameObject holder;
    #endregion

    #region Private Variables


    #endregion

    #endregion

    #region Event Subscription
    private void Start()
    {
    }

    private void OnDisable()
    {

    }
    #endregion


    private void Awake()
    {
        Init();
    }
    private void Init()
    {

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
