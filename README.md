# SubstrateNET.BasicUnityDemo
Unity Basic Demo showcasing connectivity to a Substrate Node using the [Ajuna.SDK](https://github.com/ajuna-network/Ajuna.SDK) generated projects.

There are two different examples:
1. On the `main` branch where we poll and subscribe to Block Number Changes.
2. On the `substrate-node-template` branch, where we connect to the node template, listen to `Something` changes and execute the `DoSomething` extrinsic. 

## Requirements
- Download Unity [LINK](https://unity.com/download)
- Unity version 2021.3.7f1

## 1. Run on `main` branch
- Checkout Substrate Node: [monthly-2022-11](https://github.com/paritytech/substrate/releases/tag/monthly-2022-11) 
- Run in dev mode:  ./target/release/substrate --dev
- Open project
- Open Node Connection scene
- Play project

![image](https://github.com/ajuna-network/SubstrateNET.BasicUnityDemo/blob/main/images/BlockNumberScreenshot.png?raw=true)


## 2. Run on `substrate-node-template` branch
- Checkout Substrate Node: [monthly-2022-11](https://github.com/paritytech/substrate/releases/tag/monthly-2022-11)
- Run in dev node template mode: `./target/release/node-template --dev`
- Open project
- Open Node Connection scene
- Play project
- You can see the 

![image](https://github.com/ajuna-network/SubstrateNET.BasicUnityDemo/blob/main/images/DoSomethingScreenshot.png?raw=true)


## Available Video Tutorials

 - [How to generate the necessary .NET projects using the Ajuna.SDK](https://www.youtube.com/watch?v=27k8vxCrXcY)
 - [How to generate the necessary DLLs for your Unity project](https://www.youtube.com/watch?v=c_FpDCys3YI)
 - [Walkthrough of `substrate-node-template` implementation](https://www.loom.com/share/630e4f49c21c4f438576b492298d90b8)