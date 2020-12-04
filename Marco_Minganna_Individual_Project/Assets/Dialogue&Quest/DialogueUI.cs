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
        TMP_InputField UserAnswer;
        [SerializeField]
        TextMeshProUGUI ButtonText;
        [SerializeField]
        Button MainButton;

        // Start is called before the first frame update
        void Start()
        {
           playerConversant= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            AIText.text = playerConversant.GetText();
            bool logic = playerConversant.GetIsQuestion();
            if (!logic)
            {
                UserAnswer.gameObject.SetActive(false);
                ButtonText.text = "Next >";
                
            }
           GameObject.FindGameObjectWithTag("Player").GetComponent<QuestionsAndAnswers>().StartConverstation(logic);  
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
