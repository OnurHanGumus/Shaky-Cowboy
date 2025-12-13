using System.Collections.Generic;

public class SaveGameCommand
{
    public void OnSaveData<T>(SaveDataEnums states, T newValue, string fileName = "SaveFile")
    {
        ES3.Save(states.ToString(), newValue, fileName + ".es3");
    }
}