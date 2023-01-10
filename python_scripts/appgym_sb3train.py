import ai4u
from ai4u.controllers import BasicGymController
import AI4UEnv
import gym
import numpy as np
from stable_baselines3 import DQN

env = gym.make("AI4UEnv-v0", buffer_size=81920)
model = DQN("CnnPolicy", env, verbose=1, tensorboard_log="tblogs")
model.learn(total_timesteps=2000000, log_interval=4)
model.save("model")

del model # remove to demonstrate saving and loading
