import ai4u
import AI4UEnv
import gym
from stable_baselines3 import DQN

env = gym.make("AI4UEnv-v0", buffer_size=81920)

model = DQN.load("logs/rl_model_1000000_steps")
obs = env.reset()
while True:
    action, _states = model.predict(obs, deterministic=True)
    obs, reward, done, info = env.step(action)
    env.render()
    if done:
      obs = env.reset()