# Design and Development of a Human-Agent Collaboration Model for Situation Awareness in Cockpit

## Abstract

## Keywords

> 以人为本

human-agent collaboration, context awareness, situation awareness, implicit interaction

## Introduction

> 1. 引入（发展...）
> 2. 挑战
> 3. 提出了什么（HCockpit），功能（ill图片：拟物风，agent-env形式），设计（略）
> 4. 实验 insights
> 5. 结论与贡献（框架+LLM实例）
> 6. 论文结构

This project introduces HarmonyCockpit (HCockpit), an agent architecture designed to incorporate cutting-edge large multimodal models and orchestrate human-agent collaboration (HAC) in cockpit with transparency.

> how it works

The HCockpit achieves situational awareness by integrating insights into both the internal and external environments of the cockpit and generates natural language-level tasks in context-awareness to facilitate HAC. Subsequently, HCockpit translates tasks into actions adapting to predefined cockpit model, thereby guiding both humans and agents towards coordinated action. Metaphorically speaking, within a cockpit setting that includes a human driver and an autonomous driving system (agent), the HCockpit serves as the orchestrating force between human and agent.

> illustrate the func
>
> 例如，如图[ill]所示，HCockpit 通过观察驾驶员的语言和生理数据，以及外部环境的视觉信息，生成了一个驾驶员的状态向量，然后根据这个状态向量生成了一个任务，这个任务被翻译成了一个控制建议，最终通过控制建议控制了驾驶舱内的设备。

## Background

## Design and Implementation

### Preliminaries

**Environment, Entity and Agent**：环境由其中所有的实体组成，实体之间存在互动。本文定义全局环境 consists of the external environment outside the ego vehicle and the internal environment. The external environment includes entities like the road, traffic, and other vehicles, while the internal environment includes the devices defined by cockpit model (will be discussed later), the autonomous driving system (AI driver), the human driver, and the AI copilot. 然而，实体们的智能水平不尽相同，一些实体只能机械地响应来自环境的刺激，另一些则可以通过对环境的观测形成一定的理解，进而自主规划一定的任务并执行一些列动作，以追求某些目标。为了区分二者，本文称后者为 agent。在 agent 的视角中，其所处环境中的其他非 agent 实体都是可感知或可控制的对象，当然，agent 感知或控制其他对象的能力取决于其自身。此外，一个环境中可能存在多个 agent，具有较高智能水平的 agent 通常由复杂系统构成，因此它们可能难以直接控制对方，而需要通过通信向对方传递自身的意图以实现某目标。Within the global environment, there are three types of agents: 1. AI driver, 2. human driver, and 3. AI copilot (an agent implemented using HCockpit). The first two are considered independent homomorphic agents, meaning they have the same observation space (the external environment). Differing from them, the AI copilot has global observation, which means it can perceive all entities in both the internal and external environments.

**Action**: 动作指智能体对环境中实体执行的操作，旨在实现其目标或应对环境的变化。在本文中，由于 AI driver and human driver are homomorphic agents, they share the same action space, which is the control to the motion controller of the vehicle. The AI copilot, however, has a different action space, which is to control the devices (including the motion controller) in the cockpit. 值得注意的是，在真实的驾驶情境中，人类驾驶员可能不仅会控制 motion controller，还会通过控制仪表盘、显示屏、空调等座舱设备改善驾驶体验。HCockpit 架构在设计之初考虑到了此类情况（详见**Cockpit Model**），但是为了简化问题以快速验证 HCockpit 的可用性，本文将人类驾驶员的动作限定在对 motion controller 的控制。

**HAC**: 人类与智能体的协作是指两者在共享环境中共同实现某个目标的过程。在本文中，HAC 是 AI copilot 的终极目标，指的是 AI driver 和 human driver 在共享的驾驶环境中共同实现安全、舒适、高效的驾驶体验。为了实现 HAC，AI copilot 需要将这一复杂的目标分解为一系列简单的子任务（HAC task），例如：1. 利用 AI driver 的环境感知能力增强驾驶员的态势感知，2. 结合人类驾驶员的状态为 AI driver 提供驾驶风格建议。由于 AI copilot 没有物理实体，cockpit devices 将作为其执行 HAC task 的工具（具身），这要求 AI copilot 具备对 cockpit devices 的控制能力。

**Cockpit Model**: 本文引入 cockpit model as a simplified model of the cockpit environment。Cockpit model 定义了座舱内所有设备的功能以及不同角色（agent）对这些设备的控制权限。技术上，cockpit model 提供了统一的应用程序接口（API）。这样，AI copilot 就可以精细地使用 cockpit devices 功能，以完成 HAC taskes。The cockpit model is designed to be simple and easy to understand, so that it can be used as a testbed for evaluating the HCockpit architecture. 值得注意的是，cockpit model 不涉及具体设备的控制细节，而是设计了一个简单的设备控制接口供评估和演示。

### HCockpit Architecture

> 优化下图

![schema](midterm-slide/HCockpi-arch.png)

#### Situational Awareness Module
<!-- perception module -->

HCopilot needs to continuously observe the global environment (outside + inside the cockpit) to achieve situational awareness (SA). For the internal cockpit environment, this project only considers the main driver inside the cockpit. HCopilot receives multimodal data inputs including driver's natural language commands, physiological and control data. It utilizes visual-language large models to extract semantic information of the scene and the driver's intention, then saves it as a structured state vector for observation of the human driver.

For the external environment, this project considers that HCopilot should obtain observations of the external environment through the AI driver, and therefore selects a visual-language large model fine-tuned in driving scenarios as simulator for the AI driver. This simulator provides interpretable environmental semantic information and projections of future states and events according to the vehicle's external environment. Similarly, HCopilot saves the aforementioned information as a state vector for observation of the external environment.

It should be noted that HCopilot does not observe another agent (AI driver), because when the human driver is in control of the vehicle, there is no need to consider the AI driver's strategy or intention. Conversely, when the AI driver controls the vehicle, the human driver's intentions or preferences become crucial.

#### Context Awareness Module

#### Planning Module

Orchestrate HAC tasks in context-aware:

HCopilot combines comprehensive SA of the global environment and context, serving as a messenger and coordinator for the driver and AI driver, orchestrating actions to facilitate HAC, e.g., controlling relevant hardware inside the cockpit to send alerts or information to the driver and providing control suggestions or takeover requests to the AI driver according to the driver's situation. In this process, context awareness is essential. Inspired by the work of Google RT-2, this project designed a memory module for HCockpit, enabling HCopilot to provide more comprehensive task orchestrations based on historical SA.

#### Control Module

It is worth noting that there is still a considerable gap between generating HAC tasks and controlling specific cockpit devices because it involves details of the cockpit device control interface, which is beyond the scope of this project's research. HCockpit focuses on the generation and orchestration of more general, high-level HAC tasks, so it designed a simple device control interface for evaluation and demonstration. HCopilot is an instance that has adopted the aforementioned cockpit model.

### HCopilot Implementation

To assess the efficacy of the HCockpit and garner deeper understandings, this project developed HarmonyCopilot (HCopilot) agent as an instance of the HCockpit architecture by leveraging state-of-the-art large multimodal models and incorporating advanced smart cockpit models, which will be discussed in detail below.

## Results and Discussion

## Conclusion and Future Work

### Conclusion

### Reflection

### Future Work

## References

## Acknowledgement

## Appendices

## Risk and environmental impact assessment
