# HCockpit

## Overview

HCockpit is a sophisticated agent architecture designed to enhance situation awareness in automotive cockpit environments. It integrates advanced Large Multimodal Models (LMMs) to improve communication and collaboration between human drivers and autonomous driving systems.

This project introduces HCopilot, an AI copilot agent based on the HCockpit architecture, and evaluates its performance using the GTAV simulation environment.

![HCopilot Workflow](./docs/figures/workflow.png)

For more detailed insights, refer to the [final report](./docs/Hu%20Yitong_2020213350_FinalReport_Anonymity.pdf), [presentation slides](https://gamma.app/docs/Design-and-Development-of-a-Human-Agent-Collaboration-Model-for-S-j4v4nydhp1x4dgk), and other resources in the [docs directory](./docs/).

## Getting Started

### Installation

Follow these steps to set up HCockpit on your local machine:

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Wiederholung/HCockpit.git
   ```

2. **Navigate to the Project Directory**

   ```bash
   cd HCockpit/
   ```

3. **Set Up the Environment and Dependencies**

   - Using Conda (Recommended):

     ```bash
     conda env create -f environment.yml
     conda activate HCockpit
     ```

   - Using Pip:

     ```bash
     # Ensure you are using Python 3.10.14 and have activated a virtual environment named 'HCockpit'
     pip install -r requirements.txt
     ```

4. **Configure the Environment Variables**

   - Modify the file [`.env.template`](./.env.template) following the instructions inside, and save it as `.env`.

   - Alternatively, set environment variables according to the [`.env.template`](./.env.template) file.

### Usage

- **Quick Demo**: Run all cells in the [notebook](./src/hcopilot.ipynb) to see a demo in the `change_lane` scenario.

- **Testing Different Scenarios**: Modify the `scenario` variable in the `Load Data` cell of the notebook:

  ```python
  scenarios = ["change_lane", "turn_right-r", "turn_right-l", "dead_zone", "dazzle", "phone"]
  scenario = scenarios[n]  # Change 'n' to select the scenario
  ```

  After setting the scenario, execute the `Load Data` and `Main` cells again.

- **Customizing Prompts**: To test different prompts, edit the [prompt markdown file](./hcopilot_workspace/config/prompt.md) and rerun the relevant notebook cells starting from `Orchestrate Prompt`.

For further details on how to utilize HCockpit, consult the [notebook guide](./src/hcopilot.ipynb).
