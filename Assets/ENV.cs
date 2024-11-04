// using System.Collections;
// using System.Collections.Generic;
// using System.Collections.Specialized;
// using UnityEngine.SceneManagement;
// using UnityEngine;

// using System.Linq;
// using NumSharp;
// using TorchSharp;
// using static TorchSharp.torch;

// public class ENV
// {   
//     EventManager em;
//     public int obs_dim;
//     public int act_dim;
//     string scene_name;
//     GameObject actor;
//     Transform victory;
//     float maxHeight;

//     public ENV(int _obs_dim, int _act_dim, string _scene_name, GameObject player, Transform win, EventManager EventManager)
//     {
//         Debug.Log("Environment Created");
//         this.obs_dim = _obs_dim;
//         this.act_dim = _act_dim;
//         this.scene_name = _scene_name;
//         this.actor = player;
//         this.victory = win;
//         this.em = EventManager;
//     }

//     public List<object> step(Tensor act)
//     {
//         List<object> output = new List<object>();
//         List<float> actions = act.data<float>().ToList();

//         bool done = false;
//         int max_index = 0;
//         float max = 0;
        
//         //finding the index of the actions tensor with the maximum probability
//         for (int i = 0; i < actions.Count; i++)
//         {
//             if (actions[i] > max){
//                 max_index = i;
//                 max = actions[i];
//             }
//         }

//         //do something based on index
//         switch (max_index) 
//         {
//             case 0:
//                 this.em.right();
//                 break;
//             case 1:
//                 this.em.left();
//                 break;
//             case 3:
//                 this.em.jump();
//                 break;
//             case 4:
//                 this.em.grab();
//                 break;
//             case 5:
//                 this.em.drop();
//                 break;
//         }

//         //observations - collect the observations
//         Tensor obs = this.observe();
//         List<float> observations = obs.data<float>().ToList();

//         if (observations[1] > this.victory.transform.position.y)
//         {
//             done = true;
//         }
        
//         //determine rewards
//         int rewards = this.rewards(obs);

//         output.Add(obs);
//         output.Add(done);
//         output.Add(rewards);
        
//         return output;
//     }

//     public Tensor observe()
//     {
//         List<float> obs = new List<float>();
//         GameObject[] holds = GameObject.FindGameObjectsWithTag("Hold");
//         SortedDictionary<float, GameObject> sortedDistances = new SortedDictionary<float, GameObject>();
//         List<GameObject> distances = new List<GameObject>();
//         //observe the environment
//         obs.Add(this.actor.transform.position.x);
//         obs.Add(this.actor.transform.position.y);

//         foreach (GameObject hold in holds)
//         {
//             if (hold.GetComponent<HingeJoint2D>().connectedBody == null)
//             {
//                 Vector3 directionToTarget = hold.transform.position - this.actor.transform.position;
//                 float dSqrToTarget = directionToTarget.sqrMagnitude;
//                 sortedDistances.Add(dSqrToTarget, hold);
//             }
//         }

//         foreach (KeyValuePair<float, GameObject> kvp in sortedDistances)
//         {
//             distances.Add(kvp.Value);
//         }

//         obs.Add(distances[0].transform.position.x);
//         obs.Add(distances[1].transform.position.x);
//         obs.Add(distances[2].transform.position.x);
//         obs.Add(distances[3].transform.position.x);
//         obs.Add(distances[4].transform.position.x);
//         obs.Add(distances[0].transform.position.y);
//         obs.Add(distances[1].transform.position.y);
//         obs.Add(distances[2].transform.position.y);
//         obs.Add(distances[3].transform.position.y);
//         obs.Add(distances[4].transform.position.y);
//         obs.Add(this.actor.GetComponent<RL_Controller>().grounded ? 1f : 0f);
//         obs.Add(this.actor.GetComponent<RL_Controller>().attached ? 1f : 0f);
//         obs.Add(this.actor.GetComponent<RL_Controller>().hanging ? 1f : 0f);
        
//         Tensor observations = torch.tensor(obs.ToArray());

//         return observations;
//     }

//     public int rewards(Tensor obs)
//     {
//         int reward = 0;

//         List<float> observations = obs.data<float>().ToList();
//         if (observations[1] > this.maxHeight)
//         {
//             reward += 10;
//         }

//         if (observations[1] > this.victory.transform.position.y)
//         {
//             reward += 100;
//         }

//         if (observations[1] < this.maxHeight)
//         {
//             reward -= 1;
//         }

//         return reward;
//     }

//     public Tensor reset()
//     {
//         SceneManager.LoadScene(this.scene_name);
//         return this.observe();
//     }
// }
