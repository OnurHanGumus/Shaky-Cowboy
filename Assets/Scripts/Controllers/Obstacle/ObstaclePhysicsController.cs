using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Vuruldum");
        rig.velocity = velocity;
        Debug.Log(velocity);
    }
}
