# GWBGameJam Unity 项目代码审查——修复 Prompt

> 请阅读以下 40 个 C# 文件的完整代码审查结果，根据优先级修复所有问题。项目使用 Unity 2022+，C#，单一命名空间 `GWBGameJam`。

---

## 🔴 严重问题（优先修复）

### 1. 关卡切换时怪物不清理

**文件：** `Assets/Scripts/Systems/MonsterSystem.cs`
**问题：** `MonsterSystem` 没有订阅 `OnLevelStarted`。当关卡结束时旧怪物还活在场景里，下一关开始后它们继续移动。
**修复要求：**
- 订阅 `OnLevelStarted` 事件（在 `OnEnable` 订阅、`OnDestroy` 取消）
- 处理函数中遍历 `_monsters` 数组，对所有非 null 的实例调用 `Destroy(monster.gameObject)`，然后数组全部置 null
- 注意 `OnDestroy` 中也要正确取消该事件订阅

### 2. 运行时调试代码写入磁盘

**文件：** `Assets/Scripts/Systems/MonsterSystem.cs`（L71-83）
**文件：** `Assets/Scripts/Systems/MonsterController.cs`（L27-38 及 L81-96）
**问题：** 两个文件里都有 Debug 日志代码，在每帧 UPDATE 中使用 `System.IO.File.AppendAllText` 和 `System.IO.Directory.CreateDirectory` 往项目外写 `.log` 文件。这些代码哪怕在发布版 Build 中也会运行，是 IO 污染和性能隐患。
**修复要求：**
- 将 `MonsterSystem.cs` L71-83 整块删除（注释说自己"整块删除即可移除"）
- 将 `MonsterController.cs` 中：
  - L27-38 的 `DebugLogPosition`、`DebugPositionInterval`、`_debugPosTimer` 字段删除
  - L61-69 的 Console Debug 日志块删除（`if (DebugConsole)` 那一段）
  - L120-123 的移动 Debug 日志删除
  - L151-155 的到达桌子 Debug 日志删除
- 只保留 `Debug.Log` 级别的日志，去掉所有 `System.IO` 文件操作

### 3. EventBus.Unsubscribe 在发布期间不生效

**文件：** `Assets/Scripts/Core/EventBus.cs`（L24-32）
**问题：** `Unsubscribe` 在 `_publishing == true` 时只从 `_handlersBuffer` 移除，但从不从主 `_handlers` 列表中移除。这意味着：
1. 本轮发布中该 handler 仍会被调用
2. **更严重**：取消订阅的效果永久丢失——handler 永远留在 `_handlers` 中
**修复要求：**
- 在 `Publish` 方法中，添加一个 `_pendingUnsubscribe` 列表或 mark-and-sweep 机制
- 方案：Publish 结束后，不仅把 buffer 中的 handler 加入 `_handlers`，还要移除 `_handlersBuffer` 中标记为"取消"的项。或者重新设计为不区分 add/remove buffer，统一用两个 buffer（一个待加、一个待删），Publish 结束后一起处理

---

## 🟡 中等问题

### 4. 所有 Config SO 字段暴露为 public

**文件：** 所有 `Assets/Scripts/Config/*.cs`（共 8 个 Config 文件）
- `Assets/Scripts/Config/BakingConfig.cs`
- `Assets/Scripts/Config/DoughConfig.cs`
- `Assets/Scripts/Config/DoughStateBoundaryConfig.cs`
- `Assets/Scripts/Config/GameLoopConfig.cs`
- `Assets/Scripts/Config/MonsterConfig.cs`
- `Assets/Scripts/Config/LevelConfig.cs`
- `Assets/Scripts/Config/TableConfig.cs`
- `Assets/Scripts/Config/ThrowConfig.cs`
- `Assets/Scripts/Config/LaneWaypointConfig.cs`
- `Assets/Scripts/Config/MonsterData.cs`

**问题：** 所有字段是 `public`（如 `public float UndercookedDuration = 0.5f;`），任何代码都能运行时修改 SO 数据。ScriptableObject 是数据资产，应通过只读属性暴露。
**修复要求：**
- 将每个可序列化字段改为 `[SerializeField] private`，并提供 `public` 只读属性
- 示例：
```csharp
// 改前
[Min(0.1f)] public float UndercookedDuration = 0.5f;
public float CookedDuration = 1.5f;

// 改后
[SerializeField, Min(0.1f)] private float _undercookedDuration = 0.5f;
[SerializeField, Min(0.1f)] private float _cookedDuration = 1.5f;

public float UndercookedDuration => _undercookedDuration;
public float CookedDuration => _cookedDuration;
```
- `OnValidate()` 中的修正逻辑也要改为写私有字段
- `LevelData` 和 `LaneWaypoints` 是 `[Serializable]` class 而非 SO，保留 public 但考虑加属性
- 注意：`LaneWaypointConfig` 的 `Lanes` 数组和 `RecordedStepCount` 需要被 Editor 工具写入，可以用 `[SerializeField] private` + 公开 setter 或 internal 方法

### 5. DoughStateBoundaryConfig.Validate() 过度重置

**文件：** `Assets/Scripts/Config/DoughStateBoundaryConfig.cs`（L16-29）
**问题：** `Validate()` 中只要任何一个阈值违规（比如 `ThresholdHardestToHard <= 0f`），就把 4 个阈值全部重置为默认值，而不是只修正违规的那个字段。
**修复要求：**
- 改为逐个字段检查，只修正违规的字段
- 示例伪代码：
```csharp
if (ThresholdHardestToHard <= 0f) { 修正这1个 }
else if (ThresholdMediumToHardest <= ThresholdHardestToHard) { 修正这1个 }
...
```
注意保持报告"递减"约束：`SoftToSoftest > SoftestToMedium > MediumToHardest > HardestToHard > 0`

### 6. MonsterData.TargetDoughState 显示无效枚举值

**文件：** `Assets/Scripts/Config/MonsterData.cs`
**问题：** `TargetDoughState` 在 Inspector 中显示全部 6 个枚举值（None/TooSoft/Softest/Medium/Hardest/TooHard），但策划只能选 `Softest/Medium/Hardest` 三个值。选 None/TooSoft/TooHard 会导致该种怪物永远无法被击杀。
**修复要求：**
- 编写一个 `CustomPropertyDrawer`（放在 Editor 目录中），过滤枚举下拉菜单只显示三个有效选项
- 或者在 `MonsterData` 上使用类似 `[EnumRestriction(typeof(DoughState), typeof(Softest), typeof(Medium), typeof(Hardest))]` 的方式（需要自定义 Attribute + PropertyDrawer）
- 同时在 `Awake` 中做运行时验证，无效值时 LogError 并默认设为 `Medium`

### 7. BakingSystem 状态回复逻辑可疑

**文件：** `Assets/Scripts/Systems/BakingSystem.cs`（L122-133）
**问题：** `HandleGameStateChanged` 中有一段逻辑：当从非 Playing 状态进入 Playing、且前一个状态不是 Paused、且烘焙未完成时，强行 ResetToIdle。这个路径实际很少发生，逻辑边界不清晰。
**修复要求：**
- 简化逻辑：当进入 Playing 时，如果上一状态是 LevelTransition/Death/Victory（即不是正常的 Resume），就 ResetToIdle
- 用 switch 或明确的状态白名单来判断，不要用"不是 Paused 就重置"这种反向逻辑

### 8. MonsterController 使用协程做闪白

**文件：** `Assets/Scripts/Systems/MonsterController.cs`（L171-180）
**问题：** 代码用 `StartCoroutine` + `WaitForSeconds` 实现怪物闪白。项目规范要求用 `Update + Time.deltaTime` 替代协程/Invoke，确保 TimeScale=0 时自动冻结。
**修复要求：**
- 移除协程，改为状态机 + Update 的方式
- 添加 `_wrongHitTimer`、`_wrongHitCount`、`_wrongHitPhase` 等字段
- 在 `Update` 中处理闪白计时和 Sprite 切换
- 闪白期间正常移动逻辑（Spec 说闪白和移动不互相阻塞）

### 9. MonsterConfig ScaleCurve 默认值与 Spec 不符

**文件：** `Assets/Scripts/Config/MonsterConfig.cs`（L11）
**问题：** 代码默认值是 `AnimationCurve.Linear(0, 0.01f, 7, 1.5f)`（只有两个关键帧），但 Spec 010 要求四个关键帧：(0, 0.01), (2, 0.50), (5, 1.00), (7, 1.50)。
**修复要求：**
- 在默认值中使用四个关键帧
```csharp
private static AnimationCurve DefaultScaleCurve()
{
    return new AnimationCurve(
        new Keyframe(0, 0.01f),
        new Keyframe(2, 0.50f),
        new Keyframe(5, 1.00f),
        new Keyframe(7, 1.50f)
    );
}
```
- 注意 existing asset 文件的数据不会被代码默认值覆盖

---

## 🔵 轻度问题

### 10. MonsterSystem 空的事件处理方法

**文件：** `Assets/Scripts/Systems/MonsterSystem.cs`（L126-129）
**修复要求：**
- 删除 `OnGameStateChanged` 的订阅/取消/处理方法。MonsterController 通过 TimeScale=0 自然冻结，不需要额外处理

### 11. Camera.main 使用

**文件：** `Assets/Scripts/Systems/LaneManager.cs`（L32）
**文件：** `Assets/Scripts/Core/AspectRatioEnforcer.cs`（L17）
**修复要求：**
- 改为 Serialized 引用 `[SerializeField] private Camera _camera`
- 在 Inspector 中绑定主摄像机

### 12. Prefab 命名拼写错误

**文件：** `Assets/Prefabs/Monsters/Monster_A.prefab`
- 子节点 `Vistual` → 改为 `Visual`

**文件：** `Assets/Prefabs/Projectile/Exposion.prefab`
- 文件名和 Prefab 名 `Exposion` → 改为 `Explosion`

### 13. ThrowSystem 销毁顺序

**文件：** `Assets/Scripts/Systems/ThrowSystem.cs`（L117-123）
**修复要求：**
- 将 `Destroy(_activeProjectile)` 移到 `OnThrowCompleted` 发布之后，确保订阅者能访问到面包的最后位置

### 14. MonsterController.ApplyScale() null 保护

**文件：** `Assets/Scripts/Systems/MonsterController.cs`（L143-148）
**修复要求：**
- 给 `_spriteRenderer` 添加 Awake null 检查失败时的标志位，后续操作跳过

### 15. LaneManager.GetWaypoint() 错误返回值

**文件：** `Assets/Scripts/Systems/LaneManager.cs`（L159-173）
**修复要求：**
- 将返回值改为 `bool TryGetWaypoint(int laneIndex, int posIndex, out Vector2 position)` 模式
- 或者返回 `Vector2?`（nullable），让调用者检查 `HasValue`

---

## 其他观测（非 Bug，但值得关注）

### 16. Inspect `Assets/ScriptableObjects/Configs/LaneWaypointConfig.asset`
- 确认 `RecordedStepCount` 是否与 `MonsterConfig.MoveStepCount` 一致
- 如果不一致，需要重新运行 Editor 点位计算器

### 17. 检查 Missed SO Asset
- 确保 `Assets/ScriptableObjects/Configs/MonsterConfig.asset` 存在（Grep 显示存在）
- 确认它的 `ScaleCurve` 是 4 个关键帧还是 2 个

---

## 验证清单

修复完成后逐项检查：

- [ ] 关卡切换后旧怪物全部消失，新关怪物正常生成
- [ ] `../Debug/` 目录不再生成 `.log` 文件
- [ ] EventBus 的 Unsubscribe 在发布时和发布外都正确移除 handler
- [ ] 所有 Config 字段外部只读，内部可写
- [ ] Threshold 验证只修正违规字段，不全部重置
- [ ] MonsterData 的 TargetDoughState 下拉只显示 3 个有效值
- [ ] 闪白用 Update + deltaTime 实现，不用协程
- [ ] 命名拼写修正
- [ ] 项目可在 Unity Editor 中打开、Play，无红色 Error
