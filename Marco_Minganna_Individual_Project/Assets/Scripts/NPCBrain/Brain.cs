﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    ANN ann;
    SaveandLoadWeights saveloads;
    TraintheANN training;
    List<double>[] inputsData;
    List<double>[] outputsData;


    // Start is called before the first frame update
    public void StartTheBrain(string path)
    {

        training = this.gameObject.GetComponent<TraintheANN>();
        training.path =path;
        training.SafeLoadData();
        saveloads = new SaveandLoadWeights();
        ann = new ANN(training.numberofImput, training.numberofOutput, training.numberofHidden,
                       training.numberofNperHidden, training.learningRate,training.ActivationFunction,training.ActivationFunctionO);

        if (training.train)
        {
            GetTheData();
            UseTheDataToTrain();
        }
        else
        {
            string weights = saveloads.LoadData(training.usage, training.numberofImput, training.numberofOutput);
            ann.LoadWeights(weights);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)&&training.train)
        {
            string content = ann.PrintWeights();
            Debug.Log("saving");
            saveloads.SaveWeights(training.usage, training.numberofImput, training.numberofOutput, content);
        }
    }

    List<double> Run(List<double> inputs, List<double> outputs, bool train)
    {
        if(train)
        {
            return (ann.Train(inputs, outputs));
        }
        else
        {
            return (ann.CalcOutput(inputs));
        }
    }

    public List<double> Run(List<double> inputs, bool train)
    {
        List<double> outputs = new List<double>();
        if (train)
        {
            return (ann.Train(inputs, outputs));
        }
        else
        {
            return (ann.CalcOutput(inputs));
        }
    }


    void GetTheData()
    {
        string[] inputs = training.inputsandoutputs.Split(',');
        inputsData = new List<double>[training.numberofdata];
        outputsData= new List<double>[training.numberofdata];
        Debug.Log(inputsData.Length);
        int index = 0;
        string newspace = "n";
        for (int i = 0; i < inputsData.Length; i++)
        {
            inputsData[i] = new List<double>();
            outputsData[i] = new List<double>();

        }

        foreach (string bit in inputs)
        {
            if (string.Equals(newspace.Trim(), bit.Trim()))
            {
                index += 1;
            }
            else
            {
                int number;
                int.TryParse(bit, out number);
                if (inputsData[index].Count < training.numberofImput)
                {
                    inputsData[index].Add(number);
                }
                else
                {
                    outputsData[index].Add(number);
                }

              
            }
        }
    }


    void UseTheDataToTrain()
    {
        for (int i = 0; i < training.numberoftraining; i++)
        {
            for(int j=0;j<inputsData.Length;j++)
            {
                List<double> output = new List<double>();
                output = Run(inputsData[j], outputsData[j], true);
            }
            
        }


    }


 
}
