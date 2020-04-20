using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using UnityEngine;

// Run nuget command PM  >> Install-Package Unity.Newtonsoft.Json -Version 7.0.0

public class DataReader
{
    public bool FromResources;


    public DataReader(bool FromResources = false)
    {
        this.FromResources = FromResources;
    }

    public string APP_DATA {
        get {
            return Application.persistentDataPath + "/Resources/";
        }
    }

    public string RESOURCES_PATH {
        get {
            return Application.dataPath + "/Resources/";
        }
    }

    JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Culture = CultureInfo.GetCultureInfo("pt-BR"),
        Formatting = Formatting.Indented
    };

    public void CreateJsonFromObject<T>(string jsonPath, T objToSave)
    {
        try
        {
            // Check for player folder
            if (!jsonPath.EndsWith(".json"))
            {
                jsonPath += ".json";
            }

            string fullPath;
            if (FromResources)
            {
                fullPath = RESOURCES_PATH + jsonPath;
            }
            else
            {
                fullPath = Path.Combine(APP_DATA, jsonPath);
            }
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            Debug.Log(RESOURCES_PATH);
            if (File.Exists(fullPath))
            {
                objToSave = default;
                return;
            }
            string res = JsonConvert.SerializeObject(objToSave, settings);
            File.Create(fullPath).Dispose();
            File.WriteAllText(fullPath, res);
            Debug.Log("File has been written in " + fullPath);

        }
        catch (Exception e)
        {
            Debug.LogWarning("Algo deu errado ao criar o objeto json " + e.Message);
            objToSave = default;
            return;
        }
    }

    public T GetObjectFromJson<T>(string pathToJson)
    {
        try
        {
            string jsonValue = string.Empty;
            if (FromResources)
            {
                string result = APP_DATA + pathToJson + ".json";
                jsonValue = File.ReadAllText(result);
            }
            else
            {
                TextAsset file = Resources.Load<TextAsset>(pathToJson);
                if (file != null)
                {
                    jsonValue = file.text;
                }
                else
                {
                    throw new Exception(message: "File not found in resources");
                }
            }
            T obj = JsonConvert.DeserializeObject<T>(jsonValue, settings);
            return obj;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Algo deu errado ao pegar o objeto json " + e.Message);
            return default;
        }
    }


    public T[] GetAllObjectFromJson<T>(string pathToDirectory)
    {
        try
        {
            string jsonValue = string.Empty;
            T[] objects = default;
            if (!FromResources)
            {
                string result = APP_DATA + pathToDirectory;
                if (!Directory.Exists(result))
                {
                    Directory.CreateDirectory(result);
                }
                string[] files = Directory.GetFiles(result);
                objects = new T[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    objects[i] = JsonConvert.DeserializeObject<T>(files[i], settings);
                }
            }
            else
            {
                TextAsset[] files = Resources.LoadAll<TextAsset>(pathToDirectory);
                if (files != null)
                {
                    objects = new T[files.Length];
                    int cont = 0;
                    foreach (TextAsset file in files)
                    {
                        objects[cont] = JsonConvert.DeserializeObject<T>(file.text, settings);
                        cont++;
                    }
                }
                else
                {
                    throw new Exception(message: "File not found in resources");
                }
            }
            return objects;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Algo deu errado ao pegar o objeto json " + e.Message);
            return default;
        }
    }
}
