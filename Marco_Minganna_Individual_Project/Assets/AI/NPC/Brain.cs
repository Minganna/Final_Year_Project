using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    ANN ann;
    SaveandLoadWeights saveloads;
    TraintheANN training;
    List<int>[] inputsData;
    List<int>[] outputsData;

    // Start is called before the first frame update
    void Start()
    {

        training = this.gameObject.GetComponent<TraintheANN>();
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
            Debug.Log(weights);
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
        if(Input.GetKeyDown(KeyCode.T))
        {
            TryNeuron();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(ann.PrintWeights());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            string weights = saveloads.LoadData(training.usage, training.numberofImput, training.numberofOutput);
            Debug.Log(weights);
            ann.LoadWeights(weights);
        }
    }

    List<double> Run(int part1,int part2,int part3,int answer1,int answer2,bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(part1);
        inputs.Add(part2);
        inputs.Add(part3);
        outputs.Add(answer1);
        outputs.Add(answer2);
        if(train)
        {
            return (ann.Train(inputs, outputs));
        }
        else
        {
            return (ann.CalcOutput(inputs));
        }
    }
    public List<double> Run(int part1, int part2, int part3, bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(part1);
        inputs.Add(part2);
        inputs.Add(part3);
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
        inputsData = new List<int>[training.numberofdata];
        outputsData= new List<int>[training.numberofdata];
        Debug.Log(inputsData.Length);
        int index = 0;
        string newspace = "n";
        for (int i = 0; i < inputsData.Length; i++)
        {
            inputsData[i] = new List<int>();
            outputsData[i] = new List<int>();

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
                if (inputsData[index].Count < 3)
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


    void TryNeuron()
    {
        List<double> output = new List<double>();
        output = Run(1, 1, 1, false);
        Debug.Log("1,1,1: "+ (float)output[0] + " , " + (float)output[1]);
        output = Run(0, 0, 0, false);
        Debug.Log("0,0,0:"+ (float)output[0] + " , " + (float)output[1]);
        output = Run(0,0,1, false);
        Debug.Log("0,0,1:"+ (float)output[0] + " , " + (float)output[1]);
    }

    void UseTheDataToTrain()
    {

        for (int i = 0; i < training.numberoftraining; i++)
        {
            List<double> output = new List<double>();
            output = Run(inputsData[0][0], inputsData[0][1], inputsData[0][2], outputsData[0][0], outputsData[0][1], true);
            output = Run(inputsData[1][0], inputsData[1][1], inputsData[1][2], outputsData[1][0], outputsData[1][1], true);
            output = Run(inputsData[2][0], inputsData[2][1], inputsData[2][2], outputsData[2][0], outputsData[2][1], true);
            output = Run(inputsData[3][0], inputsData[3][1], inputsData[3][2], outputsData[3][0], outputsData[3][1], true);
            output = Run(inputsData[4][0], inputsData[4][1], inputsData[4][2], outputsData[4][0], outputsData[4][1], true);
            output = Run(inputsData[5][0], inputsData[5][1], inputsData[5][2], outputsData[5][0], outputsData[5][1], true);
            output = Run(inputsData[6][0], inputsData[6][1], inputsData[6][2], outputsData[6][0], outputsData[6][1], true);
            output = Run(inputsData[7][0], inputsData[7][1], inputsData[7][2], outputsData[7][0], outputsData[7][1], true);
        }

        TryNeuron();
    }


    void ExampleData()
    {
        List<double> output = new List<double>();
        output = Run(1, 1, 1, 1, 0, true);
        output = Run(0, 0,0, 0, 1, true);
        output = Run(0, 0, 1,0, 0, true);
        output = Run(0, 1, 0, 0, 0, true);
        output = Run(0, 1,1, 0, 0, true);
        output = Run(1, 0, 0, 0, 0, true);
        output = Run(1, 0, 1, 0, 0, true);
        output = Run(1, 1, 0, 0, 0, true);
        TryNeuron();
    }

 
}
