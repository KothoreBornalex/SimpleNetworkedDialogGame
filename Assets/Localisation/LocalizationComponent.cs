using System.Collections.Generic;
using UnityEngine;
using LocalizationPackage;
using UnityEditor;

public class LocalizationComponent : MonoBehaviour
{
    [SerializeField] TextAsset TSVFile;
    [SerializeField] List<GameObjectForText> GameObjectsForText;

    Dictionary<string, string[]> MapOfTexts = new Dictionary<string, string[]>();

    bool cantBeUse = true;
    bool endInit = false;

    [System.Serializable]
    public struct GameObjectForText
    {
        public GameObject gameObject;
        public string Key;
    }

    public bool GetEndInit { get => endInit; }

    /////////////
    // METHODS //
    /////////////

    private void Start()
    {
        ReadTSVFile();

        endInit = true;
    }

    void ReadTSVFile()
    {
        if (TSVFile == null) // Is filePath empty
        {
            Debug.LogWarning("No referenced file in '" + gameObject.name + "', check if the file is correctly set. ", gameObject);
            return;
        }


        string[] lines = TSVFile.text.Split('\n');
        if (lines == null) // Is file empty
        {
            Debug.LogError("No Data in file in '" + gameObject.name + "', check if file is empty. ", gameObject);
            return;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split('\t');

            if (i == 0)
            {
                MapOfTexts.Add("Languages", columns);
            }
            else
                MapOfTexts.Add(columns[0], columns);
        }

        if (MapOfTexts["Languages"].Length <= 1 || IndexDefaultLanguage() <= -1) // Does File contains any usable Data
        {
            Debug.LogError("Unreadable data in '" + gameObject.name + "', check if languages are correclty indicated on the first line of the file and doesn't have any typo ", gameObject);
            return;
        }

        cantBeUse = false;
    }

    private string ErrorCall()
    {
        Debug.LogError("Fatal error in initial parameters of '" + gameObject.name + "', see logs for more information ", gameObject);
        return "ERROR SEE LOGS";
    }

    private int IndexCurrentLanguage()
    {
        string[] listOfLanguagues = MapOfTexts["Languages"];
        for (int i = 1; i < listOfLanguagues.Length; i++)
        {
            if (listOfLanguagues[i].Contains(LocalizationManager.Instance.GetCurrentLanguage)) { return i; }
        }

        return -1;
    }
    private int IndexDefaultLanguage()
    {
        string[] listOfLanguagues = MapOfTexts["Languages"];
        for (int i = 1; i < listOfLanguagues.Length; i++)
        {
            if (listOfLanguagues[i].Contains(LocalizationManager.Instance.GetDefaultLanguage)) { return i; }
        }

        return -1;
    }
    private int IndexGivenLanguage(string language)
    {
        string[] listOfLanguagues = MapOfTexts["Languages"];
        for (int i = 1; i < listOfLanguagues.Length; i++)
        {
            if (listOfLanguagues[i].Contains(language)) { return i; }
        }

        return -1;
    }

    public string GetText(string Key, bool keyIsLanguage = false)
    {
        if (cantBeUse) return ErrorCall();

        if (!MapOfTexts.ContainsKey(Key)) 
        { 
            Debug.LogError("No Associated key in '" + TSVFile.name + "' with key: " + Key, gameObject);
            return "NO ASSOCIATED KEY : '" + Key + "' in '" + TSVFile.name + "' "; 
        }

        int index;
        if (keyIsLanguage)
            index = IndexGivenLanguage(Key);
        else
            index = IndexCurrentLanguage();

        if (index <= -1)
        {
            index = IndexDefaultLanguage();

            if (index <= -1)
            {
                Debug.LogError(LocalizationManager.Instance.GetDefaultLanguage + ", as default language, isn't supported in '" + TSVFile.name + "' .tsv file, check if said language is correclty indicated ", gameObject);
                return "DEFAULT LANGUAGE NOT SUPPORTED : '" + LocalizationManager.Instance.GetDefaultLanguage + "' in '" + TSVFile.name + "' ";
            }
            else
            {
                Debug.LogWarning(LocalizationManager.Instance.GetCurrentLanguage + " isn't supported in '" + TSVFile.name + "' .tsv file, check if said language is correclty indicated ", gameObject);
            }
        }


        string text = "";
        if (index < MapOfTexts[Key].Length)
            text = MapOfTexts[Key][index];

        if (text == "")
        {
            index = IndexDefaultLanguage();
            if (index < MapOfTexts[Key].Length)
                text = MapOfTexts[Key][index];

            if (text == "")
            {
                Debug.LogError("Text is empty for " + Key + " key in current & default language in '" + gameObject.name + "' .tsv file, check is said text is correctly set in file ", gameObject);
                return "TEXT IS NULL : '" + LocalizationManager.Instance.GetDefaultLanguage + "' at '" + Key + "' key in '" + TSVFile.name + "' ";
            }
            else
            {
                Debug.LogWarning(LocalizationManager.Instance.GetCurrentLanguage + " text is empty for " + Key + " key in '" + gameObject.name + "' .tsv file ", gameObject);
                if (!LocalizationManager.Instance.DebugGetReturnDefault) return "TEXT IS NULL : '" + LocalizationManager.Instance.GetCurrentLanguage + "' at '" + Key + "' key in '" + TSVFile.name + "' ";
            }
        }

        return text;
    }


    /////////////////////
    /// CUSTOM EDITOR ///
    /////////////////////
    [CustomEditor(typeof(LocalizationComponent))]
    public class LocalizationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            LocalizationComponent languageComponent = (LocalizationComponent)target;

            languageComponent.TSVFile = EditorGUILayout.ObjectField("TSV File", languageComponent.TSVFile, typeof(TextAsset), true) as TextAsset;

            if (GUILayout.Button("Check"))
            {
                LocalizationWindow.ShowWindow();
            }

        }
    }


    public class LocalizationWindow : EditorWindow
    {
        string test = "hello";

        public static void ShowWindow()
        {
            GetWindow<LocalizationWindow>("Text GameObject List");
        }

        private void OnGUI()
        {
            GUILayout.Label("Label test", EditorStyles.boldLabel);

            test = EditorGUILayout.TextField("Name", test);

            if (GUILayout.Button("Close"))
            {
                Close();
            }
        }
    }
}