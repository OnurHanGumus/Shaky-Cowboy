using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ThumbleweedMovementController : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables

    #endregion
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] private float forceX, forceY;
    #endregion
    #region Protected Variables

    #endregion
    #region Private Variables
    private Rigidbody _rig;
    #endregion
    #region Properties

    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //_rig.AddRelativeTorque(new Vector3(0,0,speed));
        _rig.AddForce(new Vector3(forceX, forceY, 0), ForceMode.Force);
    }

    private void OnDisable()
    {
        _rig.velocity = Vector3.zero;
    }
}
