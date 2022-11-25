# SubstrateNET.BasicUnityDemo
Unity Basic Demo showcasing connectivity to a Substrate Node.

There are two different examples:
1. On the `main` branch where we poll and subscribe to Block Number Changes.
2. On the `substrate-node-template` branch, where we connect to the node template, listen to `Something` changes and execute the `DoSomething` extrinsic. 

## Requirements
- Download Unity [LINK](https://unity.com/download)
- Unity version 2021.3.7f1

## 1. Run on main 
- Checkout Substrate Node: [monthly-2022-11](https://github.com/paritytech/substrate/releases/tag/monthly-2022-11) 
- Run in dev mode:  ./target/release/substrate --dev
- Open project
- Open Node Connection scene
- Play project

![image](https://github.com/ajuna-network/SubstrateNET.BasicUnityDemo/blob/main/images/BlockNumberScreenshot.png?raw=true)



## 1. Run on main
- Checkout Substrate Node: [monthly-2022-11](https://github.com/paritytech/substrate/releases/tag/monthly-2022-11)
- Run in dev node template mode: `./target/release/node-template --dev`
- Open project
- Open Node Connection scene
- Play project

![image](https://github.com/ajuna-network/SubstrateNET.BasicUnityDemo/blob/main/images/DoSomethingScreenshot.png?raw=true)

