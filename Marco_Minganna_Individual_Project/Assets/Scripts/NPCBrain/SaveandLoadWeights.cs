using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Script used to create and load a text file with the ANN weights
/// </summary>
public class SaveandLoadWeights 
{
    /// <summary>
    /// Function used to export the data in a text file
    /// </summary>
    /// <param name="usage"></param>
    /// <param name="inputs"></param>
    /// <param name="outputs"></param>
    /// <param name="content"></param>
    public void SaveWeights(string usage,int inputs,int outputs,string content)
    {
        string path = Application.dataPath + "/AI/Weights/"+usage+","+inputs+","+outputs+".txt";
        var sr = File.CreateText(path);
        sr.WriteLine(content);
        sr.Close();
    }

    /// <summary>
    /// function used to load the text file
    /// </summary>
    /// <param name="usage"></param>
    /// <param name="inputs"></param>
    /// <param name="outputs"></param>
    /// <returns></returns>
    public string LoadData(string usage,int inputs, int outputs)
    {
        string path = Application.dataPath + "/AI/Weights/" + usage + "," + inputs + "," + outputs + ".txt";
        if (File.Exists(path))
        {
            var sr = File.OpenText(path);
            string line = sr.ReadLine();
            Debug.Log("loading");
            return line;
        }
        else
        {
            string empty;
            empty="";
            return empty;
        }
    }
}
