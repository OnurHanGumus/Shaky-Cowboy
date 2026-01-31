using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;

public class ObstaclePhysicsController : MonoBehaviour, IReplaceable
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PoolSignals _poolSignals { get; set; }
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] private Rigidbody rig;
    [SerializeField] private int health = 2;
    [SerializeField] private bool isUnbreakable = false;
    private int _takenDamage = 0;
    #endregion
    #region Private Variables


    #endregion
    #region Properties

    #endregion
    #endregion
    public void OnShooted(Vector3 velocity)
    {
        rig.linearVelocity = rig.linearVelocity + velocity * 0.8f ;
        if (isUnbreakable)
        {
            return;
        }
        ++_takenDamage;
        if (_takenDamage >= health)
        {
            _poolSignals.onGetObject(Enums.PoolEnums.ObstacleExplosion, transform.position).SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
