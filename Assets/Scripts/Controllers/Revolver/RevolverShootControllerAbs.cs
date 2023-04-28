using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System.Threading.Tasks;


public abstract class RevolverShootControllerAbs : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables

    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] protected Transform bulletPointTransform;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] private Transform playerLeftHand;
    [SerializeField] private RevolverMovementController movementController;
    [SerializeField] protected Vector3 revolverInitializePos;
    [SerializeField] protected Vector3 revolverInitializeRot;
    #endregion
    #region Protected Variables
    protected bool _isReloading = false;
    protected WaitForSeconds wait2_4f = new WaitForSeconds(2.4f);
    protected WaitForSeconds wait0_5f = new WaitForSeconds(0.5f);
    protected bool _isDied = false;

    #endregion
    #region Private Variables
    #endregion
    #region Properties
    public int AmmoCapacity { get; set; } = 3;
    public int CurrentBulletCount { get; set; } = 3;
    #endregion
    #endregion
    #region Event Subscription
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        revolverInitializePos = transform.localPosition;
        revolverInitializeRot = transform.localEulerAngles;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
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
        
    }

    protected void SetRevolverPosition()
    {
        transform.parent = playerTransform;
        transform.localPosition = revolverInitializePos;
        transform.localEulerAngles = revolverInitializeRot;
    }

    public virtual void Reload()
    {

    }

    public virtual IEnumerator ReloadCoroutine()
    {
        yield break; 
    }
    public virtual void OnDie(StickmanBodyPartEnums bodyPart)
    {
        RevolverOnHand();
    }

    public void OnInitializeLevel()
    {
        CurrentBulletCount = AmmoCapacity;
        _isReloading = false;
        StopAllCoroutines();
        SetRevolverPosition();
    }

    protected void RevolverOnHand()
    {
        transform.parent = playerLeftHand;
        transform.localPosition = new Vector3(-0.357f, -0.361f, 0.219f);
        transform.localEulerAngles = new Vector3(-55.102f, 130.775f, -211.855f);
    }
}
