using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public List<TextAsset> innocentDialogue, groupDialogue, spyDialogue;
    public List<FileInfo> innocentFiles, groupFiles, spyFiles;
    //public string innocentDirectory, groupDirectory, spyDirectory;

    private List<List<TextAsset>> dialogueLists = new List<List<TextAsset>>();
    private List<List<FileInfo>> dialogueFileLists = new List<List<FileInfo>>();
    private string[] speechType = new string[] {"innocent", "group", "spy"};
    public string[] directoryLocation = new string[] { "innocentDirectory", "groupDirectory", "spyDirectory" };

    public DirectoryInfo innocentDirectory = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "innocentDialogue"));
    public DirectoryInfo groupDirectory = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "groupDialogue"));
    public DirectoryInfo spyDirectory = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "spyDialogue"));
    private FileInfo[] innocentFileSet, groupFileSet, spyFileSet;
    private void Awake()
    {

        innocentFileSet = innocentDirectory.GetFiles("*.txt");
        groupFileSet = groupDirectory.GetFiles("*.txt");
        spyFileSet = spyDirectory.GetFiles("*.txt");


    }
    public string GetRandomDialogue(string type)
    {
        if (type == "Innocent")
        {
            string text = GetRandomInnocentDialogue();
            return text;
        }
        else if (type == "Group")
        {
            string text = GetRandomGroupDialogue();
            return text;
        }
        else if (type == "Spy")
        {
            string text = GetRandomSpyDialogue();
            return text;
        }
        else
        {
            string asset = "Invalid Dialogue Type";
            return asset;
        }
    }
    public string GetRandomInnocentDialogue()
    {

            int randomInt = Random.Range(0, (this.innocentFileSet.Length -1));
        string asset = ReadFile(randomInt, innocentFileSet);
       

        return asset;
    }
    public string GetRandomGroupDialogue()
    {

        FileInfo[] type = this.groupFileSet;
        var randomInt = Random.Range(0, type.Length - 1);
        string asset = ReadFile(randomInt, type);


        return asset;
    }
    public string GetRandomSpyDialogue()
    {

        FileInfo[] type = spyFileSet;
        var randomInt = Random.Range(0, type.Length - 1);
        string asset = ReadFile(randomInt, type);


        return asset;
    }

    private string ReadFile(int index, FileInfo[] type) 
    {
        var targetAsset = type[index];
        string asset = File.ReadAllText(targetAsset.FullName);
        if (asset != null)
        {
            return asset;
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    private List<TextAsset> CheckType(string type)
    {
        int index = System.Array.IndexOf(speechType, type);
        var value = dialogueLists[index];
        return value;
    }
    private List<FileInfo> CheckTypeFile(string type)
    {
        int index = System.Array.IndexOf(speechType, type);
        var value = dialogueFileLists[index];
        return value;
    }
}
