using UnityEngine;

namespace Controllers
{
    public class ClearActiveLevelCommand : MonoBehaviour
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            for (int i = 0; i < levelHolder.childCount; i++)
            {
                Destroy(levelHolder.GetChild(i).gameObject);
            }
        }
    }
}