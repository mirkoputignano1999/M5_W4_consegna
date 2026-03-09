using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereDialogue : NPCDialogue
{
    [SerializeField] private string _endSceneName = "EndScene";
    [SerializeField] private float _preFadeDelay = 2f;

    public override void StartDialogue()
    {
        base.StartDialogue();
        StartCoroutine(EndGameRoutine());
    }

    private IEnumerator EndGameRoutine()
    {
        yield return new WaitForSeconds(_preFadeDelay);


        if (DialogueUI.Instance != null && DialogueUI.Instance.IsOpen)
            DialogueUI.Instance.CloseDialogue();

        if (ScreenFader.Instance != null)
        {
            yield return ScreenFader.Instance.FadeOut();
        }

        if (!string.IsNullOrWhiteSpace(_endSceneName))
            SceneManager.LoadScene(_endSceneName);
    }
}