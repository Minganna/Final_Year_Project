using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;


public class QuestionsAndAnswers : MonoBehaviour
{
    GameObject brain;

    List<string> Questions;
    List<string> Answers;
    List<string> CorrectAnswer;
    List<string> WrongAnswer;
    List<string> PartiallyCorrect;

    int indexAnswer;

    // Start is called before the first frame update
    void Start()
    {
        brain = GameObject.FindGameObjectWithTag("Brain");
        Questions = new List<string>();
        Answers = new List<string>();
        CorrectAnswer = new List<string>();
        WrongAnswer = new List<string>();
        PartiallyCorrect = new List<string>();
        string line = LoadFile();
        Debug.Log(line);
        DivideQandA(line);
        TMP_Text question = GameObject.FindGameObjectWithTag("Question").GetComponent<TMP_Text>();
        string questionstring = question.text;
        indexAnswer=CompareQuestion(questionstring);


    }


    string LoadFile()
    {
        string path = Application.dataPath + "/AI/NPC/Questions&Answers/Questions&Answers.txt";
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
            empty = "";
            return empty;
        }
    }

    //Separate the questions and answers in 2 lists with same index
    void DivideQandA(string line)
    {
        if (line == "") return;
        string[] bit = line.Split(','); 
        bool switchqa=true;
        for(int i=0;i<bit.Length;i++)
        {
            if(switchqa)
            {
                Questions.Add(bit[i]);
                Debug.Log(Questions[i]);
            }
            else
            {
                Answers.Add(bit[i]);
                Debug.Log(Answers[i-1]);
            }
            switchqa = !switchqa;
            
        }
        
    }

    //check at what index the question is
    int CompareQuestion(string query)
    {
        Debug.Log(query);
        int index = 0;
        foreach(string q in Questions)
        {
            if(string.Equals(query.Trim(),q.Trim()))
            {
                return index;
            }
            else
            {
                index++;
            }
        }

        return -1;
        
    }

    List<int> AnswerCheck(string Submitted)
    {
        List<int> Result=new List<int>();
        TraintheANN tr = brain.GetComponent<TraintheANN>();
        string[] Submittedbit=Submitted.Split(' ');
        string[] bit = Answers[indexAnswer].Split(' ');
        if (Submittedbit.Length != tr.numberofImput &&  Submittedbit.Length != bit.Length)
        {
            for(int i=0;i<tr.numberofImput;i++)
            {
                Result.Add(0);
            }
            return Result;
        }
        else
        {
            for (int i = 0; i < tr.numberofImput; i++)
            {
                if(string.Equals(Submittedbit[i].Trim(), bit[i].Trim()))
                {
                    Result.Add(1);
                }
                else
                {
                    Result.Add(0);
                }
            }
            return Result;
        }  
    }

    public void CheckCorrectAnswer()
    {
        Brain ai = brain.GetComponent<Brain>();
        TMP_InputField UserAnswer = GameObject.FindGameObjectWithTag("Answer").GetComponent<TMP_InputField>();
        List<int> UserResult = new List<int>();
        UserResult = AnswerCheck(UserAnswer.text);
        List<double> output = new List<double>();
        output= ai.Run(UserResult[0], UserResult[1], UserResult[2], false);
        Debug.Log(UserResult[0]+","+UserResult[1]+ "," + UserResult[2]+":" + (float)output[0] + " , " + (float)output[1]);
    }
}
