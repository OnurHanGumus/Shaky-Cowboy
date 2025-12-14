using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals
{
    public UnityAction onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestart = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public UnityAction onStorePanelClicked = delegate { };
    public UnityAction onStorePanelClosed = delegate { };
    public UnityAction<UpgradeEnums,int> onUpgradePurchased = delegate { };
    public UnityAction<UpgradeEnums> onUpgradePurchasedEnded = delegate { };

    public readonly struct InputUpdate
    {
        public readonly Vector3 TerrainPos;

        public InputUpdate(Vector3 terrainPos)
        {
            TerrainPos = terrainPos;
        }
    }
}