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

    [Inject] private PoolSignals _poolSignals { get; set; }
    [Inject] protected CoreGameSignals _coreGameSignals { get; set; }
    [Inject] protected AudioSignals _audioSignals { get; set; }
    [Inject] protected IHaptic _haptic { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] protected Transform bulletPointTransform;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] private Transform playerLeftHand;
    [SerializeField] private RevolverManagerAbs manager;
    [SerializeField] protected Vector3 revolverInitializePos;
    [SerializeField] protected Vector3 revolverInitializeRot;
    [SerializeField] protected ParticleSystem smokeParicle;

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
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        revolverInitializePos = transform.localPosition;
        revolverInitializeRot = transform.localEulerAngles;
    }

    public virtual void OnShoot()
    {
        if (_isReloading)
        {
            return;
        }

        _haptic.CheckBeforePlay(HapticEnums.Fire);
        GameObject bullet = _poolSignals.onGetObject?.Invoke(PoolEnums.Bullet, transform.position);
        bullet.transform.position = bulletPointTransform.position;
        bullet.transform.eulerAngles = transform.eulerAngles;
        bullet.SetActive(true);

        smokeParicle.Play();

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
        transform.localPosition = manager.Data.RevolverHandLocalPosition;
        transform.localEulerAngles = manager.Data.RevolverEulerLocalAngle;
    }
}
