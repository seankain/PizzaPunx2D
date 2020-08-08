using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class KeyButton
{
    public KeyCode key;
    public Button btn;
}

public class GradeInputManager : MonoBehaviour
{
    public GradeDataManager dataManager;
    public List<KeyButton> keyButtons;

    int currentId;
    string currentText;

    public Text IdText;
    public Text TextText;

    public Image goodBtn;
    public Image badBtn;
    public Image normalBtn;
    public Image weirdBtn;
    public Image insaneBtn;
    public Image earlyBtn;
    public Image midBtn;
    public Image lateBtn;
    public Image checkBtn;
    public Image rejectBtn;

    public Color selectedColor;

    public void GetContentToReview()
    {
        var nextId = dataManager.GetNextReviewId(StartAtReviewNum);
        var nextContent = dataManager.GetContentText(nextId);
        
        currentId = nextId;
        currentText = nextContent;
        IdText.text = currentId.ToString();
        TextText.text = currentText;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
        GetContentToReview();
    }

    void colorButtons()
    {
        if (isGood == 1) goodBtn.color = selectedColor;
        else goodBtn.color = Color.white;

        if (isGood == 0) badBtn.color = selectedColor;
        else badBtn.color = Color.white;

        if (textInsanity == 0) normalBtn.color = selectedColor;
        else normalBtn.color = Color.white;

        if (textInsanity == 1) weirdBtn.color = selectedColor;
        else weirdBtn.color = Color.white;

        if (textInsanity == 2) insaneBtn.color = selectedColor;
        else insaneBtn.color = Color.white;

        if (textGameStage == 0) earlyBtn.color = selectedColor;
        else earlyBtn.color = Color.white;

        if (textGameStage == 1) midBtn.color = selectedColor;
        else midBtn.color = Color.white;

        if (textGameStage == 2) lateBtn.color = selectedColor;
        else lateBtn.color = Color.white;

        if (requiresReview == 1) checkBtn.color = selectedColor;
        else checkBtn.color = Color.white;

        if (reject == 0) rejectBtn.color = Color.white;
        else rejectBtn.color = selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var kb in keyButtons)
        {
            if (Input.GetKeyDown(kb.key)) kb.btn.onClick.Invoke();
        }

        colorButtons();
    }

    public int isGood;
    public int textInsanity;
    public int textGameStage;
    public int requiresReview;
    public int reject;
    public int StartAtReviewNum = 0;

    public void SetReject()
    {
        if (reject == 0)
        {
            isGood = -1;
            textInsanity = -1;
            textGameStage = -1;
            requiresReview = 0;
            reject = int.MaxValue;
        }
        else ResetValues();
    }

    public void SetIsGood(int goodBad)
    {
        Debug.Log(goodBad);
        isGood = goodBad;
    }

    public void SetInsanity(int insanity)
    {
        textInsanity = insanity;
    }

    public void SetGameStage(int gameStage)
    {
        textGameStage = gameStage;
    }

    public void SetReview()
    {
        if (requiresReview == 0) requiresReview = 1;
        else requiresReview = 0;
    }

    void ResetValues()
    {
        isGood = int.MinValue;
        textInsanity = int.MinValue;
        textGameStage = int.MinValue;
        requiresReview = 0;
        reject = 0;
    }

    public void Done()
    {
        if (isGood + textInsanity + textGameStage + reject >= 0)
        {
            var content = currentText;

            if (reject > 0) {
                content = ""; // Clear out objectionable content
                reject = 1;
            }

            var review = new Review()
            {
                content = content,
                doNotUse = reject,
                isGood = isGood,
                gameStage = textGameStage,
                insanityLevel = textInsanity,
                needsChecking = requiresReview
            };

            dataManager.AddReview(currentId, review);
            ResetValues();
            GetContentToReview();
        }
        else
        {
            Debug.Log("Not done");
        }
    }
}
