using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BulletSpriteController : MonoBehaviour
{
    [SerializeField] private GameObject bulletModel;
    private void OnEnable()
    {
        ChangeActiveness(true);
    }

    public void Disable()
    {
        ChangeActiveness(false);
    }

    private void ChangeActiveness(bool isActive)
    {
        bulletModel.SetActive(isActive);
    }
}
