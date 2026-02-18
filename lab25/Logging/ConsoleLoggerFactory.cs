using System;

namespace lab25.Logging;

public class ConsoleLoggerFactory : LoggerFactory { public override ILogger CreateLogger() => new ConsoleLogger(); }