// add score manager
using UnityEngine;
using UnityEngine.UI;

// access the Text Mesh Pro namespace
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int maxScore;

    int score; // total points
    int numHit; // total number of enemies hit

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        numHit = 0;
        scoreText.text = "Goal: " + maxScore + "\nScore: " + score;
    }

    //we will call this method from our target script
    // whenever the player collides or shoots a target a point will be added
    public void AddPoint()
    {
        score++;

        if (score < maxScore) {
            scoreText.text = "Goal: " + maxScore + "\nScore: " + score;
        }  
        else {
            scoreText.text = "Nice Job!\nScore: " + score;
        } 
    }

    public void addHit() {
        numHit++;
    }

    public int getScore() {
        return score;
    }

    public int getHits() {
        return numHit;
    }

    
}