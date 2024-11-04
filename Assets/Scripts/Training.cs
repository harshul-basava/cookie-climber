// using System.Collections;
// using System;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using UnityEngine;

// using TorchSharp;

// public class Training : MonoBehaviour
// {
//     public GameObject player;
//     public Transform win;
//     public EventManager EventManager;

//     ENV env;
//     PPO ppo;
//     FNN fnn;


//     private const int RTLD_NOW = 2;
//     // Import the LoadLibrary method from kernel32.dll
//     // Import the dlopen function from libSystem.dylib
//     [DllImport("libSystem.dylib", EntryPoint = "dlopen", SetLastError = true)]
//     private static extern IntPtr dlopen(string fileName, int flags);

//     // Import the dlclose function from libSystem.dylib
//     [DllImport("libSystem.dylib", EntryPoint = "dlclose", SetLastError = true)]
//     private static extern int dlclose(IntPtr handle);

//     string LibtorchPath = "/Users/harshulbasava/Documents/untitled/climbingGame/Assets/Packages/libtorch-cpu-osx-arm64.2.2.1.1/lib/netstandard2.0/libtorch-cpu-osx-arm64.dylib";

//     // Start is called before the first frame update
//     void Start()
//     {
//         dlopen(LibtorchPath, RTLD_NOW);
//         TorchSharp.torch.InitializeDeviceType(TorchSharp.DeviceType.CPU);
//         fnn = new FNN(15, 5);

//         // env = new ENV(15, 5, "training", player, win, EventManager);
//         // ppo = new PPO(env);

//         // ppo.learn(19200);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
