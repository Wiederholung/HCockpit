# Prompt

## SA

**输入:**

1. 带有驾驶员注视点热力图的第一人称视角图像
2. human driver control data：包含驾驶员的键盘输入，JSON 格式数据
3. external environment data: 包括半径 50 米内的车辆信息（相对坐标、速度、加速度和碰撞箱，包含 ego 车辆），JSON 格式数据
4. 历史数据：先前对话的摘要

**任务:**

结合图像分析和控制输入，生成一个结构化的报告，详细描述以下方面：

**A. 驾驶员注视点分析**：

- 识别的实体及其描述（例如，颜色、类型、距离、明显动作）

**B. control data 解读**：

- 驾驶员当前的驾驶行为

- 驾驶员的潜在驾驶意图

**C. external env data 解读**：

- 外部车辆的运动行为
- 外部车辆的潜在运动

**D. 综合分析**：

- 结合上述分析，描述驾驶员的总体状态、注意力和驾驶意图，以及外部实体可能如何影响驾驶员

**注意事项：**

- 考虑驾驶员注视点与识别实体间的关联

- 分析驾驶员控制输入与驾驶环境及注视点的一致性

- 评估驾驶员的物理状态和注意力水平如何影响驾驶性能和安全性

- 利用历史数据比较，以识别行为模式的变化或一致性

**输出示例：**

```
A. 驾驶员注视点分析：
   - 实体：红色轿车，距离约30米。
   - 实体：交通信号灯，显示红灯。

B. control data 解读：
   - 当前驾驶行为：轻微减速，准备停车。
   - 潜在驾驶意图：可能准备在交通信号灯处停车。

C. external env data 解读：
   - 红色轿车（eid=v-12345）已经停车，正在路口等待通行

D. 综合分析：
   - 驾驶员当前专注于道路状况和周围环境，准备在红灯处停车。控制输入与前方环境一致，显示出良好的驾驶反应和意图。根据历史数据，驾驶员在类似情况下通常表现出良好的注意力和驾驶技能。
```

**Input:**

1. First-person perspective image with driver’s gaze heat map
2. Human driver control data: Contains the driver’s keyboard input, JSON format data
3. External environment data: includes vehicle information within a radius of 50 meters (relative coordinates, speed, acceleration and collision box, including ego vehicles), JSON format data

**Task:**
Combines image analysis and control input to generate a structured report detailing:
**A. Driver’s gaze point analysis**:

- Recognized entities and their descriptions (e.g., color, type, distance, apparent motion)
  **B. Control data interpretation**:
- Driver's current driving behavior
- Driver's underlying driving intention
  **C. External env data interpretation**:
- Movement behavior of external vehicles
- Potential movement of external vehicles
  **D. Comprehensive analysis**:
- Combined with the above analysis, describe the driver's overall state, attention and driving intentions, and how external entities may affect the driver

**Note:**

- Consider the relationship between the driver's gaze point and the recognized entity
- Analyze the consistency of driver control inputs with driving environment and gaze points
- Evaluate how a driver's physical state and attention level affect driving performance and safety
- Utilize historical data comparisons to identify changes or consistency in behavioral patterns

**Example output:**

A. Driver’s gaze point analysis: - Entity: red car, about 30 meters away. - Entity: traffic light, showing red light.

B. Control data interpretation: - Current driving behavior: slow down slightly and prepare to stop. - Potential driving intent: Possibly preparing to stop at a traffic light.
C. external env data interpretation: - The red car (eid=v-12345) has stopped and is waiting to pass at the intersection
D. Comprehensive analysis: - The driver is currently focused on the road conditions and surroundings, preparing to stop at a red light. Control inputs are consistent with the environment ahead, showing good driving response and intent. Based on historical data, drivers generally demonstrate good concentration and driving skills in similar situations.

## 规划模块

**Prompt:**

**输入:**

1. **外部环境信息**：包括半径 30 米范围内的车辆信息（坐标、速度、加速度和碰撞箱，以字典格式提供）。

2. **驾驶员当前状态描述**：以自然语言提供的驾驶员状态分析。

3. **座舱功能列表**：详细列出了车辆座舱的智能功能，用于支持提出的动作。

   > 图片转 HUD

**任务:**

根据提供的外部环境信息和驾驶员当前状态，规划出能够帮助驾驶员的动作。这些动作应该旨在提高驾驶体验和安全性，同时必须在座舱功能列表提供的智能水平范围内。使用自然语言描述建议的动作。

**注意事项：**

- 确保建议的动作与驾驶员当前状态和外部环境信息相匹配。
- 考虑动作的紧急性和重要性，优先提出最能提高安全性的建议。
- 使用座舱功能列表中的功能来实现建议的动作。

**输出示例：**

```shell
根据外部环境信息和驾驶员当前状态，以下是智能副驾驶的建议动作：

1. **显示警告**：在车载屏幕上显示警告，提醒驾驶员左后方存在一辆接近的车辆（实体ID：123）。该车辆正以较高速度接近，可能存在变道冲突的风险。

2. **语音提示**：通过车辆的语音系统，提醒驾驶员保持当前车道，并注意前方的减速车辆（实体ID：456）。建议减速以保持安全距离。

3. **导航建议**：鉴于当前交通状况和驾驶员的目的地，建议通过车载导航系统提供一条避开高密度交通区域的备选路线。

请驾驶员根据实际情况考虑采取上述建议中的动作，以确保行车安全。
```

> 论文注意：
>
> 要有理论指导
>
> 工程文件，仿真平台
