# UI 制作 Prompt

将以下内容复制到新 Claude 会话开头即可启动 UI 制作工作流。

---

```
项目路径：E:\Program Files\GWBGameJam

请先阅读以下文件，了解项目现状：
1. E:\Program Files\GWBGameJam\CLAUDE.md
2. E:\Program Files\GWBGameJam\Docs\009_UISystem.md
3. E:\Program Files\GWBGameJam\Docs\012_Architecture.md

读完后，请帮我完成以下 4 个 UI 界面的 Unity 手动操作。用 Unity MCP 工具操作，不需要我手动做。

---

## 任务：制作 4 个 UI 界面

### 1. 开始界面 (MainMenu.scene)
- 确认 MainMenu_Canvas 的 StartButton 已正确绑定 SceneLoader.LoadGame()
- 添加游戏标题（TMP Text，如 "揉面团"）
- 调整 Canvas 为 1920x1080，Scale With Screen Size

### 2. 关卡内暂停
- GameLoop 已实现 Escape 键暂停逻辑
- UISystem 已实现 OnGameStateChanged → 切换 Canvas
- 确认 PauseMenu_Canvas 在 Game.unity 中存在

### 3. 暂停界面 (PauseMenu_Canvas)
- 给 PauseMenu_Canvas 添加半透明黑色背景 (Image, Color rgba(0,0,0,0.5))
- 添加「暂停」标题文字
- 确认 ContinueButton 的 OnClick → GameLoop.ResumeGame()
- 确认 MainMenuButton 的 OnClick → GameLoop.GoToMainMenu()

### 4. 过关界面 (LevelTransition_Canvas)
- 确认 ContinueButton 的 OnClick → GameLoop.AdvanceFromTransition()
- 确认过关文字（UISystem 会在 LEVEL_TRANSITION 状态时自动设置文本）
- 调整文字样式和按钮布局

---

## 实施顺序

1. 打开 MainMenu.scene → 完善开始界面
2. 打开 Game.scene → 检查并完善 PauseMenu_Canvas 的按钮连线
3. 检查并完善 LevelTransition_Canvas 的按钮连线
4. Play 模式测试完整流程
```
