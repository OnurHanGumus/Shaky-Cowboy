using Enums;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
namespace Controllers
{
    public class EnemyRiggingController : EnemyRiggingControllerBase
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

        public override void OnPlay()
        {
            SetSpesificRigActiveness(0, true);
            SetSpesificRigActiveness(1, true);
            rigBuilder.enabled = true;
        }
        public override void OnReload()
        {
            //ChangeAll(false);
            rigBuilder.enabled = false;
        }

        public override void OnReloaded()
        {
            //ChangeAll(true);
            rigBuilder.enabled = true;
        }

        public override void OnDie(StickmanBodyPartEnums bodyPart)
        {
            rigBuilder.enabled = false;
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