using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;

public class PongActuator : Actuator
{
    private BasicAgent basicAgent;
    public Rigidbody2D body;
    public float speed = 10;
    private Vector2 velocity = Vector2.zero;

    public override void OnSetup(Agent agent)
    {
        this.basicAgent = (BasicAgent) agent;
        this.shape = new int[1]{3};
        this.rangeMin = new float[]{};
        this.rangeMax = new float[]{};
        this.basicAgent.AddResetListener(this);
    }

    public override void Act()
    {
        if (basicAgent != null && !basicAgent.Done)
        {
            int action = (int) agent.GetActionArgAsFloatArray()[0];
            if (action == 1)
            {
                velocity = new Vector2(0, speed);
            }
            else if (action == 2)
            {
                velocity = new Vector2(0, -speed);
            }
            else
            {
                velocity = Vector2.zero;
            }
            body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
        }
    }

    public override void OnReset(Agent agent)
    {
        velocity = Vector2.zero;
    }
}
