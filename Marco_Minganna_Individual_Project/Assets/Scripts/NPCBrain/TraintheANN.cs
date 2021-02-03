using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TraintheANN : MonoBehaviour
{
    //required fields
    public bool train;
    public int numberofImput;
    public int numberofOutput;
    public int numberofHidden;
    public int numberofNperHidden;
    public double learningRate;
    public int ActivationFunction;
    public int ActivationFunctionO;
    public string usage;
    public string path;

    //training fields
    public int numberoftraining;
    public string inputsandoutputs;
    public int numberofdata;
    public bool LoadData;
    public bool SaveData;

    public void SafeLoadData()
    {
       string fullpath = Application.dataPath + "/AI/DataforTraintheANN/"+ path;
        if (File.Exists(fullpath))
        {
            var sr = File.OpenText(fullpath);
            string line = sr.ReadLine();
            Debug.Log("loading");
            string[] data = line.Split(',');
            numberofImput = int.Parse(data[0]);
            numberofOutput = int.Parse(data[1]);
            numberofHidden = int.Parse(data[2]);
            numberofNperHidden = int.Parse(data[3]);
            learningRate = double.Parse(data[4]);
            ActivationFunction = int.Parse(data[5]);
            ActivationFunctionO = int.Parse(data[6]);
            usage = data[7];
            train = bool.Parse(data[8]);
        }
    }


}


[CustomEditor(typeof(TraintheANN))]
[CanEditMultipleObjects]
public class Training : Editor
{
    SerializedProperty ListLenght;

    override public void OnInspectorGUI()
        {
            var myScript = target as TraintheANN;

            //required fields for the ANN to work
            myScript.numberofImput = EditorGUILayout.IntField("number of inputs: ", myScript.numberofImput);
            myScript.numberofOutput = EditorGUILayout.IntField("number of outputs: ", myScript.numberofOutput);
            myScript.numberofHidden = EditorGUILayout.IntField("number of Hidden: ", myScript.numberofHidden);
            myScript.numberofNperHidden = EditorGUILayout.IntField("number of neurons: ", myScript.numberofNperHidden);
            myScript.learningRate = EditorGUILayout.DoubleField("Learning Rate: ", myScript.learningRate);
            myScript.ActivationFunction = EditorGUILayout.IntField("activation per hidden: ", myScript.ActivationFunction);
            myScript.ActivationFunctionO = EditorGUILayout.IntField("activation per output: ", myScript.ActivationFunctionO);
            myScript.usage = EditorGUILayout.TextField("usage", myScript.usage);
            myScript.path=EditorGUILayout.TextField("path", myScript.path);

            //enable training if required
            myScript.train = GUILayout.Toggle(myScript.train, "Training required");
            if (myScript.train)
               {
                    myScript.numberoftraining = EditorGUILayout.IntField("number of trainings: ", myScript.numberoftraining);
                    myScript.numberofdata = EditorGUILayout.IntField("size of data array: ", myScript.numberofdata);
                    myScript.inputsandoutputs = EditorGUILayout.TextField("inputs and outputs: ", myScript.inputsandoutputs);
               }

        myScript.SaveData = GUILayout.Button("Save Data");
        myScript.LoadData = GUILayout.Button("Load Data");
       

            if(myScript.LoadData)
            {
                 myScript.SafeLoadData();
            }

            if(myScript.SaveData)
                {
                      string fullpath = Application.dataPath + "/AI/DataforTraintheANN/"+ myScript.path+".txt";
                      var sr = File.CreateText(fullpath);
                      Debug.Log("saving");
                      string content = myScript.numberofImput.ToString()+","+
                                       myScript.numberofOutput.ToString() + "," +
                                       myScript.numberofHidden.ToString()+ "," +
                                       myScript.numberofNperHidden.ToString()+ "," +
                                       myScript.learningRate.ToString()+ "," +
                                       myScript.ActivationFunction.ToString()+ "," +
                                       myScript.ActivationFunctionO.ToString()+ "," +
                                       myScript.usage + "," +
                                       myScript.train.ToString(); 
                      sr.WriteLine(content);
                      sr.Close();
                 }



    }

}
