using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveandLoadWeights 
{
    public void SaveWeights(string usage,int inputs,int outputs,string content)
    {
        string path = Application.dataPath + "/AI/Weights/"+usage+","+inputs+","+outputs+".txt";
        var sr = File.CreateText(path);
        sr.WriteLine(content);
        sr.Close();
    }

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
