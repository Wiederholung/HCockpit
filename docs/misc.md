# Misc

## Feedback

The students did an excellent mock viva in general. The presentation introduces in detail about the extent to which the tasks and goals outlined in the work-plan were finished.

It reviewed the related literature and proposed the questions to answer. The issues were solved using reasonable methods and the findings are grounded in solid evidence. The student is honest about the shortcomings of this paper.

It would be nice for the student to demonstrate the related implementations with functional software or webpage.

If there is anything to be improved, I think the student need focus on one or two major issues that have been solved by this thesis and introduces the results of experiments or simulation.

## 一些 HAC tasks 实例

1. 提高用户体验（UX）：

    1. 驾驶员试图变道、倒车或调头时根据座舱功能展示相应的影像或发起语音/文字提醒

    2. 驾驶员发起主动交互时，调动座舱相应功能满足用户需求

    3. 根据驾驶员状态/习惯，生成适合的自动驾驶偏好建议，例如：驾驶员喝水时，优先采用平稳的驾驶策略

2. 提高安全性：

    1. 驾驶员注意力水平较低时或无法应对危机时，生成提醒动作或建议自动驾驶系统接管控制权

    2. 驾驶员没有注意到周围潜在威胁时，主动展示威胁源

    3. 遇到自动驾驶系统不足以应对的复杂路况时，评估驾驶员是否具备接管条件，进而要求驾驶员接管车辆或提醒驾驶员提高注意力做好接管准备

## Main Issues

Contribution can be described as follow:

1. 首次提出了一种以 large multimodal model 为核心的 HAC for SA in cockpit 框架

2. H-A 通信机制（良好的信息/意图传递），确保了 agents 透明性，有助于提高人与 agents 的理解和信任；

3. 提高驾驶体验和安全性，有助于解决 attentional tunneling 问题

4. ...

## 任务书

1. 文献综述
2. 需求分析
3. 开发
4. 实验

## Q & A

1. why 层次型的设计？

    - 为了更好地实现 HAC，HCopilot 需要将复杂的目标分解为一系列简单的子任务（HAC tasks）
    - CoT or workflow
    - 模块化设计有助于 HCopilot 的可扩展性和可维护性
  
2. why language model？

    - 语言模型是 HCopilot 的核心，因为它可以将全局观测转化为可解释的任务，进而控制座舱设备完成 HAC tasks。语言模型的可解释性有助于审计 HCopilot 的决策，进而确保驾驶安全。
    - 语言模型的优势在于其可以处理多模态输入，例如：驾驶员的自然语言指令、生理和控制数据等，这有助于 HCopilot 从多个角度理解驾驶员的意图，进而生成更全面的任务。

3. 需要说明的是，从生成 HAC tasks 到对座舱特定设备的控制仍存在巨大的 gap，因为涉及到座舱设备控制接口的细节，这已经超出了本文的研究范畴。HCockpit 聚焦于更通用、high-level 的 HAC tasks 生成和编排，故设计了简单的设备控制接口用于评估和演示。实现 HAC tasks 到座舱设备控制的无缝衔接需要统一、规范的接口，这需要行业同仁共同的努力。
