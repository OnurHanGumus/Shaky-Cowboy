using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class LoadGameDataCommand
{
    public T OnLoadGameData<T>(SaveDataEnums saveLoadStates, string fileName = "SaveFile")
    {
        return ES3.Load<T>(saveLoadStates.ToString(), fileName + ".es3", default(T));
    }

    public bool CheckIfKeyInitialized(SaveDataEnums saveLoadStates, string fileName = "SaveFile")
    {
        return ES3.KeyExists(saveLoadStates.ToString(), fileName + ".es3");
    }
}