using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField]
        TextMeshProUGUI AIText;
        [SerializeField] 
        TextMeshProUGUI TranslationText;
        [SerializeField]
        TMP_InputField UserAnswer;
        [SerializeField]
        TextMeshProUGUI SuggestionText;
        [SerializeField]
        TextMeshProUGUI ButtonText;
        [SerializeField]
        Button MainButton;
        [SerializeField]
        GameObject SpriteMaster;
        [SerializeField]
        Button QuitButton;
        [SerializeField]
        TextMeshProUGUI ConversantName;
        [SerializeField]
        RawImage ConversantAvatar;
        

        CommonVariables cv = new CommonVariables();



        // Start is called before the first frame update
        void Start()
        {
            if (cv.getTranslation())
            {
                TranslationText.gameObject.SetActive(true);
            }
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            UpdateUI();
            CheckQuestionAndUpdate(true);
            QuitButton.onClick.AddListener(() =>
            {
                GameObject suggestionButtons = GameObject.FindGameObjectWithTag("SuggestionsMaster");
                if (suggestionButtons.GetComponentsInChildren<Button>().Length > 0)
                {
                    foreach (Button sugg in suggestionButtons.GetComponentsInChildren<Button>())
                    {
                        Destroy(sugg.gameObject);
                    }
                }
                playerConversant.Quit();
                CheckQuestionAndUpdate(false);


                Canvas cp = gameObject.GetComponentInParent<Canvas>();
                cp.gameObject.SetActive(false);
            });
        }

        private void CheckQuestionAndUpdate(bool StartUp)
        {
            bool logic = playerConversant.GetIsQuestion();
            List<string> Answers = new List<string>();
            if (!logic)
            {
                UserAnswer.gameObject.SetActive(false);
                SuggestionText.gameObject.SetActive(false);
                ButtonText.text = "Next >";

            }
            else
            {
                UserAnswer.gameObject.SetActive(true);
                SuggestionText.gameObject.SetActive(true);
                ButtonText.text = "Submit";
                
                foreach (DialogueNode choice in playerConversant.GetChoices())
                {
                    if(!string.Equals(choice.GetText().Trim(), "wrong answer".Trim()))
                    {
                        Answers.Add(choice.GetText());
                    }
                   
                }
            }
           GameObject.FindGameObjectWithTag("Player").GetComponent<QuestionsAndAnswers>().StartConverstation(logic,StartUp,Answers);
        }

        public void Next()
        {
            playerConversant.Next();
            CheckQuestionAndUpdate(false);

        }
         void UpdateUI()
        {
            gameObject.SetActive(playerConversant.isActive());
            if(!playerConversant.isActive())
            {
                return;
            }
            ConversantName.text = playerConversant.GetCurrentConversantName();
            ConversantAvatar.texture = playerConversant.GetCurrentAvatar();
            if (playerConversant.IsChoosing())
            {
                bool correctAnswer = false;
                DialogueNode wrongAns=null;
                TMP_InputField UserAnswer = GameObject.FindGameObjectWithTag("Answer").GetComponent<TMP_InputField>();
                foreach (DialogueNode choice in playerConversant.GetChoices())
                {
                        if (string.Equals(choice.GetText().Trim(), UserAnswer.text.Trim()))
                        {
                            playerConversant.SelectChoice(choice);
                            correctAnswer = true;
                        }
                        if (string.Equals(choice.GetText().Trim(), "wrong answer".Trim()))
                        {
                            wrongAns = choice;
                        }
                }
                if(correctAnswer==false)
                {
                    playerConversant.SelectChoice(wrongAns);
                    AIText.color = Color.red;
                    AIText.text = UserAnswer.text.Trim();
                    if (this.isActiveAndEnabled)
                    {
                        StopAllCoroutines();
                        StartCoroutine(TypeSentence(UserAnswer.text.Trim()));
                    }
                    MainButton.gameObject.SetActive(playerConversant.hasNext());
                    UserAnswer.text = "";
                    return;
                }
                HorizontalLayoutGroup SpriteHor = SpriteMaster.GetComponent<HorizontalLayoutGroup>();
                SpriteHor.childAlignment = TextAnchor.MiddleLeft;
                UserAnswer.text = "";
            }
            else
            {
                playerConversant.PlayVoice();
                HorizontalLayoutGroup SpriteHor = SpriteMaster.GetComponent<HorizontalLayoutGroup>();
                SpriteHor.childAlignment = TextAnchor.MiddleRight;
            }
            Debug.Log("Noice");
            AIText.color = Color.black;
            if (!TranslationText.gameObject.activeSelf)
            {
                AIText.text = playerConversant.GetText();
            }
            else
            {
                string[] temp = playerConversant.GetText().Split('/');
                AIText.text = temp[0];
                if(temp.Length>1)
                {
                    TranslationText.text = temp[1];
                }
                
            }
            if(this.isActiveAndEnabled)
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentence(playerConversant.GetText()));
            }
           
            MainButton.gameObject.SetActive(playerConversant.hasNext());
   

        }

        IEnumerator TypeSentence (string sentence)
        {
            if(!cv.getTranslation())
            {
                AIText.text = "";
                foreach (char letter in sentence.ToCharArray())
                {
                    AIText.text += letter;
                    yield return null;
                }
            }
            else
            {
                AIText.text = "";
                TranslationText.text = "";
                bool Translated=false;
                foreach (char letter in sentence.ToCharArray())
                {
                    if (letter == '/')
                    {
                        Translated = true;
                    }
                    if (Translated)
                    {
                        if (letter != '/'|| letter != '-')
                        {
                            TranslationText.text += letter;
                        }
                        if (letter == '-')
                        {
                            TranslationText.text += "\n";
                        }
                        yield return null;
                    }
                    else
                    {
                        if (letter != '/' || letter != '-')
                        {
                            AIText.text += letter;
                        }
                        yield return null;
                    }
                   
                }
            }


          
        }

        public void ChangeSuggestion(int suggestionnumber)
        {
            SuggestionText.text = "word needed: " + suggestionnumber;
        }
    }
}
