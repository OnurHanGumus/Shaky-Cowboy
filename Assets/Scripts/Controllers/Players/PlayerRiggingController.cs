using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
namespace Controllers
{
    public class PlayerRiggingController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RigBuilder rigBuilder;

        #endregion

        #endregion

        private void SetSpesificRigActiveness(int index, bool value)
        {
            rigBuilder.layers[index].active = value;
        }

        public void OnPlay()
        {
            SetSpesificRigActiveness(0, true);
            SetSpesificRigActiveness(1, true);
            rigBuilder.enabled = true;
        }
        public void OnReload()
        {
            //ChangeAll(false);
            rigBuilder.enabled = false;
        }
        public void OnReloaded()
        {
            //ChangeAll(true);
            rigBuilder.enabled = true;

        }
        private void ChangeAll(bool isActive)
        {
            for (int i = 0; i < rigBuilder.layers.Count; i++)
            {
                SetSpesificRigActiveness(i, isActive);
            }
        }

    }
}