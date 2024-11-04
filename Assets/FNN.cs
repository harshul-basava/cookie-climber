// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// using TorchSharp;
// using static TorchSharp.torch;
// using static TorchSharp.torch.nn.functional;
// using static TorchSharp.TensorExtensionMethods;
// using static TorchSharp.torch.distributions;

// using NumSharp;

// public class FNN : nn.Module<Tensor,Tensor>
// {
//     int in_dim;
//     int out_dim;

//     nn.Module<Tensor,Tensor> layer1;
//     nn.Module<Tensor,Tensor> layer2;
//     nn.Module<Tensor,Tensor> layer3;

//     public FNN(int _in_dim, int _out_dim) : base("FNN")
//     {
//         this.in_dim = _in_dim;
//         this.out_dim = _out_dim;

//         this.layer1 = nn.Linear(in_dim, 64);
//         this.layer2 = nn.Linear(64, 64);
//         this.layer3 = nn.Linear(64, out_dim);
//     }

//     public override Tensor forward(Tensor obs)
//     {
//         var activation1 = relu(layer1.forward(obs));
//         var activation2 = relu(layer2.forward(activation1));
//         var output = layer3.forward(activation2);

//         return output;
//     }
// }