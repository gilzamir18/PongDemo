using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;
using TMPro;

public class PongReward : RewardFunc
{
    public GameObject ball;
    public BasicAgent otherAgent;
    public GameObject otherDisplay;
    public TMP_Text display;

    private float acmReward;
    private BasicAgent basicAgent;

    public override void OnSetup(Agent agent)
    {
        agent.AddResetListener(this);
        basicAgent = (BasicAgent) agent;
    }

    public override void OnUpdate()
    {
        display.text = "Score: " + acmReward;;
        otherDisplay.GetComponent<PongReward>().display.text = "Score: " + (-acmReward);
        if (acmReward != 0)
        {
            otherAgent.AddReward(-acmReward, this);
            basicAgent.AddReward(acmReward, this);
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
    }
}
