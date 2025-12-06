using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class FaderController : MonoBehaviour
{
    [SerializeField] Image faderImage;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        
    }

    public void Fade(int endValue, float duration)
    {
        faderImage.DOFade(endValue,duration);
        faderImage.raycastTarget = endValue == 1 ? true : false;
    }
}