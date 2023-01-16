import ai4u
from ai4u.controllers import BasicGymController
import AI4UEnv
import gym
import numpy as np
from stable_baselines3 import DQN
from stable_baselines3.common.callbacks import CheckpointCallback
checkpoint_callback = CheckpointCallback(save_freq=100000, save_path='./logs/',
                                            name_prefix='rl_model')
env = gym.make("AI4UEnv-v0", buffer_size=81920)
model = DQN("CnnPolicy", env, verbose=1, tensorboard_log="tblogs", buffer_size=500000)
model.learn(total_timesteps=3000000, log_interval=4, callback=checkpoint_callback)
model.save("model")

del model # remove to demonstrate saving and loading
