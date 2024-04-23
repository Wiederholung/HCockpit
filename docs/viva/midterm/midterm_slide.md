# Slides

## Background

The advent of fully autonomous driving is fast approaching. However, until self-driving systems can adeptly manage a diversity of situational challenges, environmental variables, and unforeseen circumstances, the journey toward complete autonomy will be evolutionary, marked by the necessity for human oversight. Concurrently, as autonomous driving technology evolves, an array of new challenges arises. Presently, numerous Original Equipment Manufacturers (OEMs) are adopting Level 2+ or Level 3 autonomous driving capabilities that permit drivers to temporarily relinquish control of specific driving functions, thereby harmonizing vehicular performance with cost-effectiveness.

However, such systems may intermittently necessitate human re-engagement in vehicle operation, and conversely, drivers might require support from autonomous systems in particular scenarios. Achieving fluid communication and collaboration between the driver and the autonomous system is paramount at this level of autonomous driving to enhance driving experience and safety.

Consequently, the industry is in pursuit of a sophisticated, which is core topic of this project, the interactive system—one that fosters situational awareness through the amalgamation of insights from both the vehicle's interior and the external environment, acting as a reciprocal link between the autonomous system and the driver, and orchestrating actions in context-aware to facilitate human-agent collaboration tasks.

## Finished Work

### Requirement Analysis

The demand for human-agent cooperation in smart cockpit is mainly reflected in improving the user experience and safety of driving:

1. Improve user experience (UX):
   1. When the driver attempts to change lanes, reverse or turn around, the corresponding image will be displayed or a voice/text reminder will be initiated according to the cockpit function.
   2. When the driver initiates active interaction, the corresponding functions of the cockpit are mobilized to meet user needs.
   3. Generate suitable autonomous driving preference suggestions based on driver status/habits, for example: when the driver drinks water, give priority to a smooth driving strategy.
2. Improve security:
   1. When the driver’s attention level is low or unable to respond to a crisis, a reminder action is generated or the autonomous driving system is recommended to take over control.
   2. When the driver is not aware of potential threats around him, he proactively displays the source of the threat.
   3. When encountering complex road conditions that the autonomous driving system is not capable of handling, assess whether the driver is qualified to take over, and then ask the driver to take over the vehicle or remind the driver to pay more attention and prepare to take over.

### Demo Design

[picture]

### System Architecture Design

[picture]

## Next Step

1. **Conduct Experiments to Evaluate the System**

   **Using Simulators**: Choose simulators like **GTA V** or **CARLA** for its realistic urban environments, sophisticated physics engine, and dynamic weather conditions. These features provide a diverse range of driving scenarios, including pedestrian interactions, traffic congestion, and emergency situations, ideal for testing the HCockpit system.

   **Integration**: Develop an integration layer that allows the HCockpit to communicate with the simulator. This layer translates the simulator's sensory data into inputs the HCockpit can understand and vice versa.

   **Evaluation Metrics**: Define key performance indicators (KPIs) such as response time to external events, accuracy in executing given tasks, ease of human-agent collaboration, and system reliability under different conditions.

2. **Develop a Demo**

   **Scenario Planning**: Create a variety of scenarios within the simulator that highlight the HCockpit's capabilities, such as emergency takeover, assisting human to turn around.

   **Interactivity**: Ensure the demo allows for real-time interaction, giving users the ability to issue commands or change parameters and observe the system's response.Visualization: Use visualization tools to clearly demonstrate the HCockpit’s situational awareness, decision-making process, and action execution. This could include visual overlays on the simulator’s output showing detected objects, planned paths, and decision points.

   **Visualization**: Use visualization tools to clearly demonstrate the HCockpit’s situational awareness, decision-making process, and action execution. This could include visual overlays on the simulator’s output showing detected objects, planned paths, and decision points.