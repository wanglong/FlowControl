# CZ-FlowControl
.Net 版本的流控服务，支持TPS、请求总数、请求延迟等流控策略。
# .Net Version
.Net Core 2.1.0
# API
var strategy = new FlowControlStrategy(){
      ID = "TPSFC",      
      Name = "服务TPS限流",     
      StrategyType = FlowControlStrategyType.TPS,
      IntThreshold = 100,
      IsRefusedRequest = false
};

FlowControlService.FlowControl(strategy, 1);



