# Spec Coding 工作流 — 启动 Prompt

将以下内容粘贴到新的 Claude 会话开头即可启动工作流。

---

## 粘贴内容（复制下方代码块全部内容）

```
你好，我需要你帮我在一个 Unity 2D 游戏项目中进行开发。

项目路径：D:\Unity Workplace\GWBGameJam

首先请执行以下操作（按顺序）：
1. 阅读 D:\Unity Workplace\GWBGameJam\CLAUDE.md（项目工作指南）
2. 阅读 D:\Unity Workplace\GWBGameJam\TASK_LOG.txt（了解当前开发进度）
3. 阅读 D:\Unity Workplace\GWBGameJam\DecisionLog.md（了解已有的设计决策）

读完后，用 2-3 句话告诉我：
- 项目当前处于什么阶段
- 下一步应该做什么

不要开始任何编码或 Spec 修改，等我确认后再行动。
```

---

## 使用说明

### 第一次使用（了解项目状态）
直接粘贴上方 prompt，Claude 会自动读取项目文档并汇报当前状态。

### 继续编码任务
粘贴 starter prompt 后，再说明具体任务，例如：
```
继续实现 LaneSystem，从 LaneManager.cs 开始。
```

### 修改需求
粘贴 starter prompt 后，说明想修改的内容：
```
我想把桌子 HP 从计数制改为百分比制，请先分析影响。
```
Claude 会遵守工作流，先分析影响再行动。

### 不需要解释 Spec Coding 工作流
所有规则已写入 CLAUDE.md，Claude 加载项目后自动遵守：
- 编码前先分析影响
- 每条指令后更新 TASK_LOG.txt
- 编码前读 013_CodingRules.md
- 每个 Task 后执行 014_ReviewChecklist.md

---

## 当前阶段说明（2026-06-27）

**编码全部完成（T01–T18），剩余全为手动 Unity 操作：**

| Task | 内容 | 状态 |
|------|------|------|
| T19 | Prefab 制作（Monster_A/B/C、Bread）| ⬜ 手动 |
| T20 | Inspector 连线（含 2 处 GameLoop 修复）| ⬜ 手动 |
| T21 | 球道点位烘焙（LaneCalculatorWindow）| ⬜ 手动 |
| T22 | 端到端集成测试 | ⬜ 手动 |

手动操作步骤详见 `Docs/ManualInstructions.md`。
美术素材需求详见 `SPEC_QA_AND_ASSETS.txt`。

---

## 常见场景 Prompt 参考

**查看手动操作步骤：**
```
打开 Docs/ManualInstructions.md，告诉我 T19 的具体操作步骤。
```

**运行时报错求助：**
```
Unity 报了这个错误：[粘贴报错信息]
帮我定位原因并修复。
```

**Inspector 连线不知道填什么：**
```
T20 中 ThrowSystem 的 _throwOrigin 字段应该填什么？
```
（Claude 会查代码和 Spec 给出准确答案）

**数值调整（Balance 类）：**
```
把第一关的生成间隔从 9 秒改为 6 秒。
```
（Claude 会直接告诉你改哪个 ScriptableObject 字段，无需动代码）

**设计变更（Gameplay 类）：**
```
我想让怪物逃跑时桌子不扣 HP，只有碰到桌子才扣。
请先分析这个改动影响哪些 Spec 和系统，不要直接改代码。
```
（Claude 会先分析，等你确认后再修改 Spec 和代码）

**T22 测试发现 Bug：**
```
测试路径 2（正确击杀）走不通，面包飞出去但怪物没有消失，Console 无报错。
帮我排查。
```
