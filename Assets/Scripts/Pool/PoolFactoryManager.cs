using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using Signals;

public class PoolFactoryManager : MonoBehaviour
{
    #region Self Variables
    #region Injections
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    #endregion
    #region Serialized Variables
    [SerializeField] private List<IPool> factoryList;
    [SerializeField] private List<PooledObject> pooledObjects;

    [System.Serializable]
    public struct PooledObject
    {
        public PoolEnums PoolEnums;
        public int Amounts;
    }
    #endregion

    #region Private Variables
    private Dictionary<PoolEnums, List<GameObject>> _poolDictionary;
    #endregion
    #endregion

    [Inject]
    public void Construct(List<IPool> factoryList)
    {
        this.factoryList = factoryList;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        #region Zenject Factory Pool
        _poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();

        for (int i = 0; i < factoryList.Count; i++)
        {
            InitializePool((PoolEnums)i, pooledObjects[i].Amounts);
        }
        #endregion

        #region Non-zenject Pool
        #endregion
    }
    #region Event Subscriptions

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
            tmp = factoryList[(int)type].OnCreate();

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
        for (int i = 0; i < factoryList.Count; i++)
        {
            ResetPool((PoolEnums)i);
        }
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in _poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
