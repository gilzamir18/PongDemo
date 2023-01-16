using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;
using TMPro;

public class PongReward : RewardFunc
{
    public GameObject ball;
    public BasicAgent targetAgent;
    public BasicAgent otherAgent;
    public bool winReward = true;
    public TMP_Text display;
    public TMP_Text otherDisplay;

    private float acmReward;
    private BasicAgent basicAgent;

    public override void OnSetup(Agent agent)
    {
        agent.AddResetListener(this);
        basicAgent = (BasicAgent) agent;
    }

    public override void OnUpdate()
    {
        if (acmReward != 0)
        {
            if (winReward)
            {
                targetAgent.AddReward(acmReward, this);
                display.text = "Score: " + acmReward;
                otherDisplay.text = "Score: -" + acmReward;
                otherAgent.Done = true;
            }
            else
            {
                targetAgent.AddReward(-acmReward, this);
                display.text = "Score: -" + acmReward;
                otherDisplay.text = "Score: " + acmReward;
                otherAgent.Done = true;
            }
        }
        acmReward = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == ball)
        {
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ball.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
            acmReward += 1;
        }
    }

    public override void OnReset(Agent agent)
    {
        acmReward = 0.0f;
        display.text = "Score: 0";
        otherDisplay.text = "Score: 0";
    }
}
