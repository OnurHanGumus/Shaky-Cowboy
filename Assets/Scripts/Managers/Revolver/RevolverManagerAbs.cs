using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;
using Data.ValueObject;
using Data.UnityObject;

public abstract class RevolverManagerAbs : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables

    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion
    #region Public Variables
    public RevolverData Data;

    #endregion
    #region Serializefield Variables
    [SerializeField] protected RevolverMovementController movementController;
    [SerializeField] protected RevolverShootControllerAbs revolverShootControllerAbs;
    #endregion
    #region Protected Variables
    #endregion
    #region Private Variables
    #endregion
    #region Properties

    #endregion
    #endregion
    #region Event Subscription

    private RevolverData GetData() => Resources.Load<CD_Revolver>("Data/CD_Revolver").Data;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Data = GetData();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void SubscribeEvents()
    {
        CoreGameSignals.onPlay += movementController.OnPlay;
        CoreGameSignals.onRestart += movementController.OnRestart;
        CoreGameSignals.onLevelInitialize += revolverShootControllerAbs.OnInitializeLevel;
    }

    protected virtual void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= movementController.OnPlay;
        CoreGameSignals.onRestart -= movementController.OnRestart;
        CoreGameSignals.onLevelInitialize -= revolverShootControllerAbs.OnInitializeLevel;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion
}
