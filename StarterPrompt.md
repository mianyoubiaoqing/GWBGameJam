# 🍞 揉面团小游戏 — 完整开发 Prompt

将以下内容粘贴到新的 Claude 会话开头即可启动工作流。

---

## 粘贴内容（复制下方全部）

```
你好，我需要你帮我在一个 Unity 2D 游戏项目中进行开发。

项目路径：E:\Program Files\GWBGameJam

首先请执行以下操作（按顺序）：
1. 阅读 E:\Program Files\GWBGameJam\CLAUDE.md
2. 阅读 E:\Program Files\GWBGameJam\TASK_LOG.txt
3. 阅读 E:\Program Files\GWBGameJam\DecisionLog.md
4. 阅读 E:\Program Files\GWBGameJam\Docs/013_CodingRules.md

读完后告诉我：
- 项目当前处于什么阶段
- 下一步应该做什么

不要开始编码，等我确认后再行动。
```

---

## 当前阶段（2026-06-28 会话后）

### 已完成
- T01–T18：编码全部完成
- T19–T21：手动操作完成（Prefab + 部分 Inspector 连线 + 点位烘焙尝试）
- T22 部分测试进行中

### 未解决的 Bug / 待办

| 问题 | 说明 | 优先级 |
|------|------|--------|
| Main Camera Tag | 未设为 MainCamera，LaneManager NRE | **高** |
| Monster 不显示 | MonsterData 的 IdleSprite/HitSprite 未赋值（无美术资源）| **高** |
| 球道点位错误 | LaneCalculator 依赖 PolygonCollider2D 算出的坐标不对 | **高** |
| UI 参考线 + Indicator | RatioBar 的参考线和指示器未创建/连线 | **中** |
| DoughSystem 推动机制 | 目前左键加粉右键加水独立增减，未实现互相推动 | **中** |
| 加载多关卡 | 加一个 Debug 按钮手动通关当前关卡 | **低** |
| Font | LiberationSans 不支持中文，过关文本暂改英文 | **低** |

### 完整游戏设计

**操作：**
- 左键点击：加随机 ½-1 格面粉，面粉条右移，推动水条左移退回
- 右键长按：随机 1-3 倍速度加水，水条左移，推动面粉条右移退回
- 空格长按：烤制（0-0.5s 不熟 / 0.5-1.5s 熟 / 1.5s+ 烤焦 / 2.5s 强制丢出）
- 空格松开：丢出面包，抛物线飞向烤制时鼠标悬停的球道

**界面：**
- 下半屏：揉面桌 + 比例条
- 上半屏：5 条透视保龄球道（上窄下宽梯形）
- 比例条：左=水，右=面粉，5 条参考线（太软/3:2/1:1/1:2/太硬），每条约 1/2 格容错区间
- 球道悬停：放大 + 高亮 Sprite

**怪物：**
- 3 种怪物（A/B/C），分别对应 3:2/1:1/1:2 档位
- 随机球道 + 随机类型生成
- 每固定时间（如 1s）移动 1 格
- 碰到桌子扣血，一击必杀

**场景流程：**
- MainMenu → Level1 → Level2 → Level3 → Victory
- 每个关卡独立场景（Level1/2/3.unity）
- 过关显示 LevelTransition，Death 显示 DeathScreen

---

## 常见 Prompt 参考

**查看当前手动操作步骤：**
```
打开 Docs/ManualInstructions.md，告诉我有哪些未完成的连线项。
```

**修 Bug：**
```
Play 时报 NPE：LaneManager.Update 第 80 行，帮我修。
```

**改 DoughSystem：**
```
把左键加面粉改为随机 ½-1 格，并实现加水时推动面粉条退回。
先分析影响，不要直接改代码。
```

**调数值：**
```
把烤制时间从 0.5/1.5/2.5 改为 0.3/1.0/2.0。
```

**加功能：**
```
在 HUD 上添加一个 Debug 按钮，点击直接通关当前关卡。
```
