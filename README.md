# CZ-FlowControl
.Net 版本的流控服务，支持TPS、请求总数、请求延迟等流控策略。
# .Net Version
.Net Core 2.1.0
# API
```csharp
//TPS限流
var strategy = new FlowControlStrategy(){
      ID = "TPSFC",      
      Name = "服务TPS限流",     
      StrategyType = FlowControlStrategyType.TPS,
      IntThreshold = 100,
      IsRefusedRequest = false
};

FlowControlService.FlowControl(strategy, 1);
```
```csharp
//访问总量限流
var strategy = new FlowControlStrategy()
{
      ID = "SumFC",
      Name = "服务总访问量限流",
      Creator = "Teld",
      LastModifier = "Teld",
      CreateTime = DateTime.Now,
      LastModifyTime = DateTime.Now,
      StrategyType = FlowControlStrategyType.Sum,
      IntThreshold = 100,
      TimeSpan = FlowControlTimespan.Second,
      IsRefusedRequest = true
};

FlowControlService.FlowControl(strategy, 1);
try
{
      for (int i = 0; i < 300; i++)
      {
           FlowControlService.FlowControl(strategy, 1);
      }
}
catch (Exception e)
{
      Assert.AreEqual(e.Message, "触发流控！");
}

```

