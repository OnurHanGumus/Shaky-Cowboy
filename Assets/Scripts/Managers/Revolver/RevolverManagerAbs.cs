using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;


public abstract class RevolverManagerAbs : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables

    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion
    #region Public Variables
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
    private void Awake()
    {
        Init();
    }

    private void Init()
    {

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
