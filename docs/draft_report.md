# System Design

## Introduction

This project presents the HarmonyCockpit (HCockpit), a framework designed to incorporate cutting-edge multi-modal large models and orchestrate human-agent collaboration (HAC) in cockpit with transparency.

The HCockpit achieves situational awareness by integrating insights into both the internal and external environments of the cockpit and generates natural language level tasks in context-aware to facilitate HAC. And then HCockpit converts tasks into actions adapting to predefined cockpit functions to inform both human and agent to act. 形象地说，在由人类驾驶员-自动驾驶系统（agent）组成的座舱环境中，HCockpit 就是人和 agent 的协调员。

为了评估 HCockpit 并取得见解，本项目开发了 HarmonyCopilot (HCopilot) 作为 HCockpit 框架的实例，其采用了 state-of-the-art multi-modal large models and 先进的的智能座舱模型。

## Preliminaries

本项目对环境和智能体的动作做出以下定义。定义全局环境由 ego 车辆舱外环境和舱内环境组成。定义全局环境中有三类智能体，分别是自动驾驶系统（AI 驾驶员）、人类驾驶员和 HCopilot。前两者被视为独立的同构智能体，即拥有相同的观测空间（舱外环境）和动作空间（控制车辆）；区别于前两者，HCopilot: 1. 拥有全局观测，2. 输出的动作是控制座舱设备。HCopilot 的核心由 language models 驱动，其策略是可解释和可审计的，以确保驾驶安全。

## System Architecture Design

### Input: 全面的态势感知 towards global environment

HCopilot需要持续观测全局环境（舱外+舱内）来完成态势感知（SA）。对于舱内环境，本项目只考虑舱内主驾驶员。HCopilot 接收包含：驾驶员自然语言指令、生理和控制数据在内的多模态数据输入，利用视觉-语言大模型提取场景的语义信息和驾驶员意图，随后保存为结构化的状态向量，作为对人类驾驶员的观测；

对于舱外环境，本项目认为 HCopilot 应该通过AI 驾驶员获取对外部环境的观测，因此本项目选择了在驾驶场景精调后的视觉-语言大模型作为 AI 驾驶员的仿真器。该仿真器根据车辆外部环境提供可解释的环境语义信息和 projection of future states and events。类似地，HCopilot 保存上述信息为状态向量，作为对外部环境的观测。

需要说明，HCopilot 不会观测另一个智能体—AI 驾驶员的状态，因为人类驾驶员主导控制车辆时不需要考虑 AI 驾驶员的策略或意图；反过来，AI 驾驶员主导控制时，人类驾驶员的意图或偏好则至关重要。

### Decision: orchestrate HAC in context-aware

HCopilot 将结合对于全局环境全面 SA 和上下文，作为驾驶员和AI 驾驶员的信使和协调者，orchestrates actions to facilitate HAC, e.g., 控制座舱内相关硬件向驾驶员发送提醒或信息并根据驾驶员的态势向AI 驾驶员提供控制建议或接管请求。在这一过程中，context awareness 是至关重要的。受 (Brohan et al., n.d.) 工作的启发，本项目设计了 HCockpit 的 memory 模块，使得 HCopilot 可以根据历史 SA 给出更 comprehensive 的任务编排。

值得注意的是，从生成 HAC tasks 到对座舱特定设备的控制仍存在巨大的 gap，因为涉及到座舱设备控制接口的细节，这已经超出了本项目研究范畴。HCockpit 聚焦于更通用、high-level 的 HAC tasks 生成和编排，故设计了简单的设备控制接口用于评估和演示。HCopilot 便是采用了上述座舱模型的实例。实现 HAC tasks 到座舱设备控制的无缝衔接需要统一、规范的接口，这需要行业同仁共同的努力。

> 一些 HAC tasks 实例：

1.  提高用户体验（UX）：

    1.  驾驶员试图变道、倒车或调头时根据座舱功能展示相应的影像或发起语音/文字提醒

    2.  驾驶员发起主动交互时，调动座舱相应功能满足用户需求

    3.  根据驾驶员状态/习惯，生成适合的自动驾驶偏好建议，例如：驾驶员喝水时，优先采用平稳的驾驶策略

2.  提高安全性：

    1.  驾驶员注意力水平较低时或无法应对危机时，生成提醒动作或建议自动驾驶系统接管控制权

    2.  驾驶员没有注意到周围潜在威胁时，主动展示威胁源

    3.  遇到自动驾驶系统不足以应对的复杂路况时，评估驾驶员是否具备接管条件，进而要求驾驶员接管车辆或提醒驾驶员提高注意力做好接管准备

## Contribution

Contribution can be described as follow:

1.  首次提出了一种以 multi-modal large model 为核心的 HAC for SA in cockpit 框架

2.  H-A 通信机制（良好的信息/意图传递），确保了 agents 透明性，有助于提高人与 agents 的理解和信任；

- 有助于解决 attentional tunneling 问题

1.  驾驶体验和安全性

2.  。。。
