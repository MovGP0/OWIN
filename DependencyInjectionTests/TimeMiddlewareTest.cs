using System;
using DependencyInjection;
using FluentAssertions;
using Microsoft.Owin.Testing;
using NSubstitute;
using Xunit;

namespace DependencyInjectionTests
{
    public class TimeMiddlewareTest
    {
        [Theory]
        [InlineData(2016, 04, 01, 00, 00, 00)]
        [InlineData(2017, 05, 02, 11, 11, 11)]
        [InlineData(2018, 06, 03, 12, 13, 14)]
        public void MustAppendCurrentTime(int year, int month, int day, int hours, int minutes, int seconds)
        {
            var now = new DateTime(year, month, day, hours, minutes, seconds, DateTimeKind.Utc);
            var clock = Substitute.For<IClock>();
            clock.TimeNow.Returns(now);

            var startup = new Startup(clock);

            using (var server = TestServer.Create(startup.Configuration))
            {
                var response = server.HttpClient.GetAsync("/");
                var headers = response.Result.Headers.GetValues("X-Time");
                headers.Should().Contain(now.ToString("o"));
            }
        }
    }
}
