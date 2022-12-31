using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;

public class PongBotController : Controller, IAgentResetListener
{
        public string actuatorName = "pong";
        public GameObject ball;
        public float sensibility = 0.5f;

        private float prevBallX = 0;
        private BasicAgent basicAgent;
        private bool started = false;

        override public string GetAction()
        {
            float actionValue = 0;
            string actionName = actuatorName;

            if (!started || basicAgent.Done)
            {
                started = true;
                return ai4u.Utils.ParseAction("__restart__");
            }

            var d = Mathf.Abs(ball.transform.position.x -  prevBallX);
            if (d > sensibility)
            {
                prevBallX = ball.transform.position.x;
                if (ball.transform.position.y > gameObject.transform.position.y)
                {
                    actionValue = 1;
                }
                else if (ball.transform.position.y < gameObject.transform.position.y)
                {
                    actionValue = 2;
                }
            }
            if (Input.GetKey(KeyCode.R))
            {
                actionName = "__restart__";
            }

            if (actionName != "__restart__")
            {
                //Debug.Log(ai4u.Utils.ParseAction(actionName, actionValue));
                return ai4u.Utils.ParseAction(actionName, new float[]{actionValue});
            } else
            {
                return ai4u.Utils.ParseAction("__restart__");
            }
        }

        public override void OnSetup()
        {
            agent.AddResetListener(this);
            prevBallX = ball.transform.position.x;
            basicAgent = (BasicAgent) agent;
        }

        public void OnReset(Agent agent)
        {
            prevBallX = ball.transform.position.x;
        }

        public override void NewStateEvent()
        {

        }
}
