using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Review {
    public int doNotUse;
    public string content;
    public int isGood;
    public int insanityLevel;
    public int gameStage;
    public int needsChecking;
}

public class GradeDataManager : MonoBehaviour
{
    public string sourceFile;
    public string destinationFile;
    
    char delimiter = '₧';

    public Dictionary<int, Review> reviews;
    public Dictionary<int, string> sourceText;

    void Awake()
    {
        sourceText = new Dictionary<int, string>();
        reviews = new Dictionary<int, Review>();
        LoadSourceData();
        LoadReviews();
    }

    public int GetNextReviewId(int minId = 0)
    {
        if (reviews.Count == 0) return minId;
        var maxReview = reviews.Keys.Max();
        var maxSource = sourceText.Keys.Max();
        if (maxReview < maxSource) return Math.Max(minId, maxReview + 1);
        return 0;
    }

    public string GetContentText(int id)
    {
        return sourceText[id];
    }

    public void AddReview(int id, Review review)
    {
        reviews.Add(id, review);
        SaveReviews();
    }

    private void SaveReviews()
    {
        var fileContent = JsonConvert.SerializeObject(reviews);
        File.WriteAllText(destinationFile, fileContent);
    }

    private void LoadReviews()
    {
        if (File.Exists(destinationFile))
        {
            var text = File.ReadAllText(destinationFile);
            reviews = JsonConvert.DeserializeObject<Dictionary<int, Review>>(text);
        }
    }

    private void LoadSourceData()
    {
        if (File.Exists(sourceFile))
        {
            var buffer = File.ReadAllText(sourceFile);
            var bits = buffer.Split(delimiter);
            for (var i = 0; i < bits.Length-2; i += 2) // length adjustment due to trailing delimiter
            {
                var bit = bits[i].Trim();
                var content = bits[i + 1].Trim();
                var bitNum = int.Parse(bit);
                sourceText.Add(bitNum, content);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
