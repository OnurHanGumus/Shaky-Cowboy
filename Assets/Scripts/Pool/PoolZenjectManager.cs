using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using Signals;

public class PoolZenjectManager : MonoBehaviour
{
    #region Self Variables
    #region Injections
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    [Inject] private BulletManager.Factory bulletFactory;
    [Inject] private TumbleweedManager.Factory tumbleweedFactory;
    
    #endregion
    #region Serialized Variables
    [SerializeField] private List<IPool> factoryList;

    [SerializeField] private int amountBullet = 20;
    [SerializeField] private int amountTumbleweed = 3;
    #endregion

    #region Private Variables
    private Dictionary<PoolEnums, List<GameObject>> _poolDictionary;
    #endregion
    #endregion
    #region Event Subscriptions
    private void Awake()
    {
        Init();
    }
    private void Init()
    {

        #region Zenject Factory Pool
        _poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();
        factoryList = new List<IPool>();

        factoryList.Add(bulletFactory);
        factoryList.Add(tumbleweedFactory);

        InitializePool(PoolEnums.Bullet, amountBullet);
        InitializePool(PoolEnums.Tumbleweed, amountTumbleweed);
        #endregion

        #region Non-zenject Pool
        #endregion
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onGetObject += OnGetObject;
        CoreGameSignals.onRestart += OnReset;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onGetObject -= OnGetObject;
        CoreGameSignals.onRestart -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializePool(PoolEnums type, int size) //Enumlarýn sýralamasý önemlidir.
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < size; i++)
        {
            tmp = factoryList[(int) type].OnCreate();
            
            tmp.SetActive(false);
            tmp.transform.parent = transform;
            tempList.Add(tmp);
        }
        _poolDictionary.Add(type, tempList);
    }

    public GameObject OnGetObject(PoolEnums type, Vector3 position)
    {
        for (int i = 0; i < _poolDictionary[type].Count; i++)
        {
            if (!_poolDictionary[type][i].activeInHierarchy)
            {
                _poolDictionary[type][i].transform.position = position;

                return _poolDictionary[type][i];
            }
        }
        return ExplandPool(type, position);
    }

    private GameObject ExplandPool(PoolEnums type, Vector3 position)
    {
        GameObject expandObject = factoryList[(int)type].OnCreate();
        expandObject.SetActive(false);
        expandObject.transform.position = position;
        expandObject.transform.parent = transform;
        _poolDictionary[type].Add(expandObject);

        return expandObject;
    }

    private void OnReset()
    {
        //reset
        ResetPool(PoolEnums.Bullet);
        ResetPool(PoolEnums.Tumbleweed);
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in _poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
