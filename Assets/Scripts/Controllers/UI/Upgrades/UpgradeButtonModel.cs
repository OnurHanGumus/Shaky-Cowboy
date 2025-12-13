using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

class UpgradeButtonModel
{
    public TextMeshProUGUI LevelText { get; set; }
    public TextMeshProUGUI ValueText { get; set; }
    public TextMeshProUGUI DescriptionText { get; set; }
    public TextMeshProUGUI PriceText { get; set; }
    public Image Image { get; set; }
    public UpgradeEnums UpgradeEnum { get; set; }
    public SaveDataEnums SaveDataEnum { get; set; }

    //private int _price;
    //public int Price
    //{
    //    get
    //    {
    //        _price = int.Parse(PriceText.text);
    //        return _price; 
    //    }
    //}
}