// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;
// using System;

// using TorchSharp;
// using static TorchSharp.torch;
// using static TorchSharp.torch.nn.functional;
// using static TorchSharp.TensorExtensionMethods;
// using static TorchSharp.torch.distributions;

// using NumSharp;

// public class PPO
// {
//     FNN actor;
//     FNN critic;
//     ENV env;
//     int obs_dim;
//     int act_dim;
//     int timesteps_per_batch;
//     int max_timesteps_per_episode;
//     int n_updates_per_iteration;
    
//     float gamma;
//     float clip;
//     float lr;

//     Tensor cov_var;
//     Tensor cov_mat;

//     TorchSharp.Modules.Adam actor_optim;
//     TorchSharp.Modules.Adam critic_optim;

//     public PPO(ENV env)
//     {
//         Debug.Log("Initializing PPO");
//         PPO self = this;
//         //dimensions of the observations and actions
//         this.obs_dim = env.obs_dim;
//         this.act_dim = env.act_dim;
//         this.env = env;
        
//         //initializing the actor and critic networks
//         this.actor = new FNN(this.obs_dim, this.act_dim);
//         this.critic = new FNN(this.obs_dim, 1);
//         this._init_hyperparameters();

//         this.actor_optim = torch.optim.Adam(self.actor.parameters(), lr=self.lr);
//         this.critic_optim = torch.optim.Adam(self.critic.parameters(), lr=self.lr);

//         this.cov_var = torch.full(this.act_dim, 0.5);
// 		this.cov_mat = torch.diag(this.cov_var);
//     }

//     public void _init_hyperparameters()
//     {
//         this.timesteps_per_batch = 4800;
//         this.max_timesteps_per_episode = 1600;
//         this.n_updates_per_iteration = 5;
//         this.gamma = 0.95f;
//         this.clip = 0.2f;
//         this.lr = 0.005f;
//     }

//     public void learn(int total_timesteps)
//     {
//         Debug.Log("Starting Learn");
//         int t_so_far = 0; //timesteps so far
//         int i_so_far = 0; //iterations so far

//         while (t_so_far < total_timesteps)
//         {
//             List<object> rollout = this.rollout();
//             Tensor batch_obs = (Tensor) rollout[0];
//             Tensor batch_acts = (Tensor) rollout[1];
//             Tensor batch_log_probs = (Tensor) rollout[2];
//             Tensor batch_rtgs = (Tensor) rollout[3];
//             List<int> batch_lens = (List<int>) rollout[4];

//             t_so_far += batch_lens.Sum();

//             i_so_far += 1;

//             List<Tensor> evaluation = this.evaluate(batch_obs, batch_acts);
//             Tensor value = evaluation[0];
//             Tensor A_k = batch_rtgs - value.detach();

//             A_k = (A_k - A_k.mean()) / (A_k.std() + 1e-10);

//             for (int _ = 0; _ < this.n_updates_per_iteration; _++)
//             {
//                 List<Tensor> eval= this.evaluate(batch_obs, batch_acts);
//                 Tensor V = eval[0];
//                 Tensor curr_log_probs = eval[1];

//                 Tensor ratios = torch.exp(curr_log_probs - batch_log_probs);

//                 var surr1 = ratios * A_k;
//                 var surr2 = torch.clamp(ratios, 1 - this.clip, 1 + this.clip) * A_k;

//                 var actor_loss = (-torch.min(surr1, surr2)).mean();
//                 var critic_loss = TorchSharp.torch.nn.functional.mse_loss(V, batch_rtgs);

//                 this.actor_optim.zero_grad();
// 				actor_loss.backward();
// 				this.actor_optim.step();

//                 this.critic_optim.zero_grad();
// 				critic_loss.backward();
// 				this.critic_optim.step();
//             }
//         }
//     }

//     public List<object> rollout()
//     {
//         List<Tensor> batch_obs = new List<Tensor>();
//         List<Tensor> batch_acts = new List<Tensor>();
//         List<Tensor> batch_log_probs = new List<Tensor>();
//         List<object> batch_rews = new List<object>();
//         List<object> batch_rtgs = new List<object>();

//         List<int> batch_lens = new List<int>();
//         List<int> ep_rews = new List<int>();

//         var t = 0;

//         while (t < this.timesteps_per_batch)
//         {
//             ep_rews = new List<int>();

//             int ept = 0;
            
//             // reset the scene and get the initial observation - TO DO
//             Tensor obs = this.env.reset();
//             bool done = false;

//             for (int ep_t = 0; ep_t < this.max_timesteps_per_episode; ep_t++)
//             {
//                 //increment timesteps
//                 t = t + 1;
//                 ept = ep_t;

//                 //track obs in current batch
//                 batch_obs.Add(obs);

//                 //get the action - TO DO
//                 List<Tensor> getAction = this.get_action(obs);
//                 Tensor action =  getAction[0];
//                 Tensor log_prob = getAction[1];

//                 //get the new observation and reward - TO DO
//                 List<object> step = this.env.step(action);
//                 obs = (Tensor) step[0];
//                 done = (bool) step[1];
//                 int rew = (int) step[2];
                
//                 //update rewards, actions and log probs
//                 ep_rews.Add(rew);
//                 batch_acts.Add(action);
//                 batch_log_probs.Add(log_prob);

//                 if (done) {break;}
//             }

//             batch_lens.Add(ept + 1);
//             batch_rews.Add(ep_rews);
//         }

//         // Reshape data as tensors in the shape specified in function description, before returning
//         Tensor batch_obs_tensor = torch.stack(batch_obs);
// 		Tensor batch_acts_tensor = torch.stack(batch_acts);
// 		Tensor batch_log_probs_tensor = torch.stack(batch_log_probs);
         
// 		Tensor batch_rtgs_tensor = this.compute_rtgs(batch_rews);
// 		// self.logger['batch_rews'] = batch_rews
// 		// self.logger['batch_lens'] = batch_len
        
//         List<object> output = new List<object>();

//         output.Add(batch_obs_tensor);
//         output.Add(batch_acts_tensor);
//         output.Add(batch_log_probs_tensor);
//         output.Add(batch_rtgs_tensor);
//         output.Add(batch_lens);

//         return output;
//     }

//     public Tensor compute_rtgs(List<object> batch_rews)
//     {
//         List<float> _batch_rtgs = new List<float>();

//         batch_rews.Reverse();

//         foreach (List<int> ep_rews in batch_rews)
//         {
//             float discounted_reward = 0;
            
//             ep_rews.Reverse();

//             foreach (int rew in ep_rews)
//             {
//                 discounted_reward = rew + (discounted_reward * this.gamma);
//                 _batch_rtgs.Insert(0, discounted_reward);
//             }
//         }

//         Tensor batch_rtgs = torch.tensor(_batch_rtgs.ToArray());

//         return batch_rtgs;
//     }

//     public List<Tensor> evaluate(Tensor batch_obs, Tensor batch_acts)
//     {
//         Tensor V = this.critic.forward(batch_obs).squeeze();

//         var mean = this.actor.forward(batch_obs);
//         var dist = torch.distributions.MultivariateNormal(mean, this.cov_mat);
//         Tensor log_probs = dist.log_prob(batch_acts);

//         List<Tensor> output = new List<Tensor>();
        
//         output.Add(V);
//         output.Add(log_probs);

//         return output;
//     }

//     public List<Tensor> get_action(Tensor obs)
//     {
//         var mean = this.actor.forward(obs);
//         var dist = torch.distributions.MultivariateNormal(mean, this.cov_mat);
       
//        var _action = dist.sample();
//        var _log_prob = dist.log_prob(_action);
//        List<Tensor> output = new List<Tensor> {torch.nn.functional.softmax(_action.detach(), dim: 0), _log_prob.detach()};
       
//        return output;
//     }
// }