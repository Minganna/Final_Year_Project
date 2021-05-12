using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UI;
using System;

/// <summary>
/// Class that connect the dialogue answer logic to the brain
/// </summary>
public class QuestionsAndAnswers : MonoBehaviour
{
    /// <summary>
    /// Keep track of the brain gameobject
    /// </summary>
    GameObject brain;
    string Question;
    /// <summary>
    /// list that stores the user answer
    /// </summary>
    List<string> Answers;
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv;

    int bitLenght=0;
    /// <summary>
    /// The prefab for the suggestions button appearing on top of the screen
    /// </summary>
    [SerializeField] Button SuggestionButton;
    
    List<string[]> bit;

    // Used to start the logic used in the dialogue
    public void StartConverstation(bool logic, bool StartUp, List<string> ans)
    {
    
        ConversationSetUP(logic,StartUp,ans);
        if(logic)
        {
            StartBrain();
        }
  
    }
    /// <summary>
    /// Part of Start conversation, used to set up the dialogue and prepare the brain for accepting inputs if needed
    /// </summary>
    /// <param name="logic"></param>
    /// <param name="StartUp"></param>
    /// <param name="ans"></param>
    private void ConversationSetUP(bool logic,bool StartUp, List<string> ans)
    {
        brain = GameObject.FindGameObjectWithTag("Brain");
        if (logic)
        {
            Answers = new List<string>();
            Answers = ans;
            TMP_Text question = GameObject.FindGameObjectWithTag("Question").GetComponent<TMP_Text>();
             Question = question.text;
        }
        if(StartUp)
        {
            cv = new CommonVariables();
            cv.UICanvas = GameObject.Find("UICanvas");
            cv.UICanvas.SetActive(false);
            cv.GameCanvas = GameObject.Find("InGameCanvas");
        }
        if(logic)
        {
            bit = GetInput();
            CreateSuggestionButtons();
        }
       
      
    }

    public CommonVariables GetCV()
    {
        return cv;
    }
   
    /// <summary>
    /// prepare the number of input and output needed for the ANN
    /// </summary>
    void StartBrain()
    {
        Brain getB = brain.GetComponent<Brain>();
        int inputs = bit[0].Length;
        Debug.Log("Number of Imputs now:"+inputs);
        int outputs;
        if(inputs>1)
        {
            outputs = 2;
        }
        else
        {
            outputs = 1;
        }

        string path = inputs + "Inputs" + outputs + "OutputsData.txt";
        getB.StartTheBrain(path);
        FindObjectOfType<DialogueUI>().ChangeSuggestion(inputs);


    }
    /// <summary>
    /// Create the suggestions buttons on the top of the screen based from the prefab
    /// </summary>
   private void CreateSuggestionButtons()
    {
        List<string[]> Allsuggest = GetInput();
        List<string> suggest= GetSuggest(Allsuggest);
        int count = suggest.Count;
        for (int i=0;i<count;i++)
        {
            GameObject parent = GameObject.FindGameObjectWithTag("SuggestionsMaster");
            GameObject button = Instantiate(SuggestionButton.gameObject, parent.transform.position, Quaternion.identity);
            int spawn = UnityEngine.Random.Range(0, suggest.Count - 1);

            button.transform.SetParent(parent.gameObject.transform, false);
            button.GetComponentInChildren<TMP_Text>().text = suggest[spawn];
            suggest.Remove(suggest[spawn]);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                TMP_InputField UserAnswer = GameObject.FindGameObjectWithTag("Answer").GetComponent<TMP_InputField>();
                if (UserAnswer.text.Contains((button.GetComponentInChildren<TMP_Text>().text)))
                {
                    return;
                }
                else
                {
                    UserAnswer.text += button.GetComponentInChildren<TMP_Text>().text;
                    UserAnswer.text += " ";
                }

            })
            ;
        }
    }

    private List<string> GetSuggest(List<string[]> allsuggest)
    {
        List<string> sgt=new List<string>();
        foreach(string[] s in allsuggest)
        {
            foreach(string a in s)
            {
                if(sgt.Count>0)
                {
                   if(!sgt.Contains(a))
                    {
                        sgt.Add(a);
                    }
                }
                else
                {
                    sgt.Add(a);
                }
            }
        }
        return sgt;
    }

    List<string[]> GetInput()
    {
        List<string[]> splitteda = new List<string[]>();
        foreach(string a in Answers)
        {
            splitteda.Add(a.Split(' '));
        }    
        
       return splitteda;
    }
    /// <summary>
    /// Part of the logic used to check if the answer submitted by the player is correct
    /// </summary>
    /// <param name="Submitted"></param>
    /// <returns></returns>
    List<double> AnswerCheck(string Submitted)
    {
        List<double> TrueResult= new List<double>();
        int correctguess = 0;
        TraintheANN tr = brain.GetComponent<TraintheANN>();
        string[] Submittedbit=Submitted.Split(' ');
        foreach(string[] a in bit)
        {
            int TempCorrectguess = 0;
            bitLenght = a.Length;
            List<double> Result=new List<double>();
            if (Submittedbit.Length != tr.numberofImput || Submittedbit.Length != bitLenght)
            {
                for (int i = 0; i < tr.numberofImput; i++)
                {
                    Result.Add(0);
                }
                return Result;
            }
            else
            {
                for (int i = 0; i < tr.numberofImput; i++)
                {
                    if (string.Equals(Submittedbit[i].Trim(), a[i].Trim()))
                    {
                        Result.Add(1);
                        TempCorrectguess += 1;
                    }
                    else
                    {
                        Result.Add(0);
                    }
                    
                    if(Result.Count== bitLenght)
                    {
                        if (TempCorrectguess== bitLenght)
                        {
                            return Result;
                        }
                        else if(TempCorrectguess>correctguess)
                        {
                            correctguess = TempCorrectguess;
                            TrueResult.Clear();
                            TrueResult = Result;
                        }
                        else if (TempCorrectguess < correctguess)
                        {
                            TrueResult = Result;
                        }
                    }
                }
           
            }
        }
        return TrueResult;
    }
    /// <summary>
    /// initial part of the funtion used to check the answer given by the player
    /// </summary>
    public void CheckCorrectAnswer()
    {
        string name = GameObject.FindGameObjectWithTag("NextSubmitButton").GetComponent<TMP_Text>().text;
        if(name!= "Next >")
        {
            GameObject parent = GameObject.FindGameObjectWithTag("SuggestionsMaster");
            Button[] children = parent.GetComponentsInChildren<Button>();
            foreach (Button c in children)
            {
                Destroy(c.gameObject);
            }
            Brain ai = brain.GetComponent<Brain>();
            TMP_InputField UserAnswer = GameObject.FindGameObjectWithTag("Answer").GetComponent<TMP_InputField>();
            List<double> UserResult = new List<double>();
            UserResult = AnswerCheck(UserAnswer.text);
            List<double> output = new List<double>();
            output = ai.Run(UserResult, false);
            string debug = "";
            for (int i = 0; i < UserResult.Count; i++)
            {
                debug += UserResult[i] + ",";
            }
            UpdateUI(output);
        }
        else
        {

            UpdateUI();
        }
        
    }


    /// <summary>
    /// After the answer is checked update the UI to proceed with the dialogue
    /// </summary>
    public void UpdateUI()
    {
        DialogueUI updateui = GameObject.FindGameObjectWithTag("DialogueMaster").GetComponent<DialogueUI>();
        updateui.Next();
    }
    /// <summary>
    /// After the answer is checked update the UI to proceed with the dialogueAfter the answer is checked update the UI to proceed with the dialogue used if more than one output
    /// </summary>
    /// <param name="output"></param>
    void UpdateUI(List<double> output)
    {
        Debug.Log((float)output[0] + " , " + (float)output[1]);
        DialogueUI updateui = GameObject.FindGameObjectWithTag("DialogueMaster").GetComponent<DialogueUI>();
        updateui.Next();
    }
}
