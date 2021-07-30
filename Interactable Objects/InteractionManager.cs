using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour
{
    protected static DialogManager DialogManager;
    protected static OptionsManager OptionsManager;
    protected static InputManager InputManager;
    protected static StateManager StateManager;
    protected static GameManager GameManager;

    protected static float ScrollingCooldown = 0.2f;

    protected int CurrentOption = 0;

    public virtual void Awake()
    {
        SetManagers();
    }

    private static void SetManagers()
    {
        if (!DialogManager) DialogManager = FindObjectOfType<DialogManager>();
        if (!InputManager) InputManager = FindObjectOfType<InputManager>();
        if (!StateManager) StateManager = FindObjectOfType<StateManager>();
        if (!GameManager) GameManager = FindObjectOfType<GameManager>();
        if (!OptionsManager) OptionsManager = FindObjectOfType<OptionsManager>();
    }

    public void InspectionEnter()
    {
        StartCoroutine(StartInspection());
    }

    protected abstract IEnumerator StartInspection();
    
    protected IEnumerator IterateTexts(string[] texts)
    {
        if (texts.Length == 0) yield break;

        for (int i = 0; i < texts.Length; i++)
        {
            if (i == texts.Length - 1)
            {
                yield return StartCoroutine(DialogManager.DisplayText(texts[i], true));
                yield return new WaitForSeconds(ScrollingCooldown);
                yield return StartCoroutine(WaitForAction());
            }
            else
            {
                yield return StartCoroutine(DialogManager.DisplayText(texts[i], false));
                yield return new WaitForSeconds(ScrollingCooldown);
                yield return StartCoroutine(WaitForAction());
            }
        }
    }

    protected void DisplayText(string text)
    {
        if (text != null)
        {
            StartCoroutine(DialogManager.DisplayText(text, false));
        }
    }

    protected IEnumerator AskQuestion(string question, string[] options)
    {
        CurrentOption = 0;

        StateManager.SetState(StateManager.DecisionState);

        yield return StartCoroutine(DialogManager.DisplayText(question, false));
        
        OptionsManager.DisplayOptions(options);

        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        OptionsManager.ActivateSelectedSound();

        StateManager.SetState(StateManager.InspectionState);

        CurrentOption = OptionsManager.ExitOptionsBox();
    }

    protected IEnumerator WaitForAction()
    {
        bool done = false;
        while (!done)
        {
            if (InputManager.Action())
            {
                done = true;
            }
            yield return null;
        }
    }
}