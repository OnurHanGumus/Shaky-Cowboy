using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MyAdInterstitalManager : AdInterstitialManager
{
    protected int _adSkipValue = 3;
    protected int _counter = 0;
    public override void ShowAd()
    {
        if (++_counter >= _adSkipValue)
        {
            base.ShowAd();
            _counter = 0;
        }
    }
}