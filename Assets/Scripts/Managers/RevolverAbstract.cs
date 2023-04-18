using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;

public abstract class RevolverAbstract : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    //[Inject] private PlayerSettings PlayerSettings { get; set; }
    //[Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] protected Transform bulletPointTransform;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] private RevolverMovementController movementController;
    [SerializeField] protected Vector3 revolverInitializePos = new Vector3(0.069f, 1.123f, -0.11f);
    [SerializeField] protected Vector3 revolverInitializeRot = new Vector3(-3.195f, -10.45f, 0.45f);
    #endregion
    #region Protected Variables
    protected bool _isReloading = false;

    #endregion
    #region Private Variables
    #endregion
    #region Properties
    public int AmmoCapacity { get; set; } = 3;
    public int CurrentBulletCount { get; set; } = 3;
    #endregion
    #endregion
    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void SubscribeEvents()
    {
        CoreGameSignals.onPlay += movementController.OnPlay;
        CoreGameSignals.onRestart += movementController.OnRestart;
        CoreGameSignals.onLevelInitialize += OnInitializeLevel;
    }

    protected virtual void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= movementController.OnPlay;
        CoreGameSignals.onRestart -= movementController.OnRestart;
        CoreGameSignals.onLevelInitialize -= OnInitializeLevel;
    }


    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    public virtual void OnShoot()
    {
        if (_isReloading)
        {
            return;
        }

        GameObject bullet = PoolSignals.onGetObject?.Invoke(PoolEnums.Bullet, transform.position);
        bullet.transform.position = bulletPointTransform.position;
        bullet.transform.eulerAngles = transform.eulerAngles;
        bullet.SetActive(true);
        --CurrentBulletCount;
        if (CurrentBulletCount <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    protected void SetRevolverPosition()
    {
        transform.parent = playerTransform;
        transform.localPosition = revolverInitializePos;
        transform.localEulerAngles = revolverInitializeRot;
    }

    public virtual IEnumerator Reload()
    {

        yield break; 
    }

    public void OnInitializeLevel()
    {
        SetRevolverPosition();
    }
}
