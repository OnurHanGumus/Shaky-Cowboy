using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;

public class ObstaclePhysicsController : MonoBehaviour, IReplaceable
{
    #region Self Variables
    #region Inject Variables
    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] private Rigidbody rig;
    #endregion
    #region Private Variables


    #endregion
    #region Properties

    #endregion
    #endregion
    public void OnShooted(Vector3 velocity)
    {
        rig.linearVelocity = rig.linearVelocity + velocity * 0.8f ;
    }
}
