﻿using FlubuCore.Context;
using FlubuCore.Tasks.NetCore;
using Xunit;

namespace Flubu.Tests.Tasks
{
    public class DotnetPublishUnitTests : TaskUnitTestBase
    {
        private readonly DotnetPublishTask _task;

        public DotnetPublishUnitTests()
        {
            _task = new DotnetPublishTask();
            _task.DotnetExecutable("dotnet");
            Tasks.Setup(x => x.RunProgramTask("dotnet")).Returns(RunProgramTask.Object);
        }

        [Fact]
        public void ConfigurationFromBuildPropertiesTest()
        {
            Properties.Setup(x => x.Get<string>(BuildProps.BuildConfiguration, null)).Returns("Release");
            _task.ExecuteVoid(Context.Object);
            Assert.Equal("-c", _task.Arguments[0]);
            Assert.Equal("Release", _task.Arguments[1]);
        }

        [Fact]
        public void ConfigurationFromFluentInterfaceConfigurationTest()
        {
            _task.Configuration("Release");
            _task.ExecuteVoid(Context.Object);
            Assert.Equal(2, _task.Arguments.Count);
            Assert.Equal("-c", _task.Arguments[0]);
            Assert.Equal("Release", _task.Arguments[1]);
        }

        [Fact]
        public void ConfigurationFromFluentInterfaceWithArgumentsTest()
        {
            _task.WithArguments("--configuration", "Release");
            _task.ExecuteVoid(Context.Object);
            Assert.Equal("--configuration", _task.Arguments[0]);
            Assert.Equal("Release", _task.Arguments[1]);
        }
    }
}