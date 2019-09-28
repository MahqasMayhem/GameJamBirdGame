using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public List<TextAsset> innocentDialogue, groupDialogue, spyDialogue;
    public List<FileInfo> innocentFiles, groupFiles, spyFiles;
    public string innocentDirectory, groupDirectory, spyDirectory;

    private List<List<TextAsset>> dialogueLists = new List<List<TextAsset>>();
    private string[] speechType = new string[] {"innocent", "group", "spy"};
    public string[] directoryLocation = new string[] { "innocentDirectory", "groupDirectory", "spyDirectory" };
    private void Start()
    {
        dialogueLists.Add(innocentDialogue);
        dialogueLists.Add(groupDialogue);
        dialogueLists.Add(spyDialogue);
       innocentFiles = CollectFiles(innocentDirectory, innocentFiles);
        groupFiles = CollectFiles(groupDirectory, groupFiles);
        spyFiles = CollectFiles(spyDirectory, spyFiles);

    }

    public TextAsset GetRandomDialogue(string speech)
    {

        var type = CheckType(speech);
            var randomInt = Random.Range(0, type.Count-1);
            TextAsset asset = ReadFile(randomInt, type);


        return asset;
    }

    private List<FileInfo> CollectFiles(string directory, List<FileInfo> fileList) //scan the given directory and gather them into a list
    {
        var info = new DirectoryInfo(directory);
        

        foreach (FileInfo file in info.GetFiles())
        {
            Debug.Log(file.Name);
            fileList.Add(file);
        }
        return fileList;
    }
    private TextAsset ReadFile(int index, List<TextAsset> type) //read the
    {
        TextAsset asset = type[index];
        return asset;
    }

    private List<TextAsset> CheckType(string type)
    {
        int index = System.Array.IndexOf(speechType, type);
        var value = dialogueLists[index];
        return value;
    }
}
