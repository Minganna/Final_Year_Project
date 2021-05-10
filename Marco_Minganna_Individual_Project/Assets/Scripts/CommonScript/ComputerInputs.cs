﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.UI;

/// <summary>
/// Class used to interact with the NPC
/// </summary>
public class ComputerInputs : MonoBehaviour
    {

    // Turn on the bit using an OR operation:
    public void Show()
    {
        Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Enviroment");
    }

    // Turn off the bit using an AND operation with the complement of the shifted int:
    private void Hide()
    {
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Enviroment"));
    }

    // Update is called once per frame
    void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, 100))
                {
                    CommonVariables cv = this.GetComponent<QuestionsAndAnswers>().GetCV();
                    // whatever tag you are looking for on your game object


                    if (raycastHit.collider.CompareTag("NPCconversant"))
                    {

                    if (cv.UICanvas != null && !cv.UICanvas.activeSelf)
                    {
                        Hide();
                        Dialogue.Dialogue[] options = raycastHit.collider.GetComponent<NPCDialogues>().GetDialogues();
                        int Index;
                        if (cv.getTranslation())
                        {
                            Index = 1;
                        }
                        else
                        {
                            Index = 0;
                        }
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>().StartDialogue(raycastHit.collider.GetComponent<NPCDialogues>(), options[Index]);             
                        cv.UICanvas.SetActive(true);
                        cv.GameCanvas.SetActive(false);
                    }
                    return;
                }
                }
            }
        }
    }
