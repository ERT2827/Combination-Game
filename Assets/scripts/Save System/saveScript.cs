using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveScript
{
    public static void SavePlayer(usableInformation info){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.TF";

        FileStream stream = new FileStream(path, FileMode.Create);

        saveFormat data = new saveFormat(info);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static saveFormat LoadPlayer(){

        string path = Application.persistentDataPath + "/data.TF";

        if(File.Exists(path)){

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            saveFormat saved = formatter.Deserialize(stream) as saveFormat;
            stream.Close();

            return saved;

        }else{
            Debug.Log("No Files?");
            return null;
        }

    }
}