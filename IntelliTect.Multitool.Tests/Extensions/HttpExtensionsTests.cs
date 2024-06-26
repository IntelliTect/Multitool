﻿using Xunit;

namespace IntelliTect.Multitool.Extensions.Tests;

public class HttpExtensionsTests
{
    static HttpExtensionsTests()
    {
        HttpClient = new HttpClient();
    }

    private static HttpClient HttpClient { get; }

    [Theory]
    [InlineData("https://thisisaverybadurlthatwillnotworkhopefullyever.com/", false)]
    [InlineData("https://www.google.com/", true)]
    [InlineData("https://api.github.com/meta", true)]
    public async Task ValidateUri_CheckUrl_SuccessIsAsExpected(string urlUnderTest, bool expected)
    {
        Assert.Equal(expected, await HttpClient.ValidateUri(new Uri(urlUnderTest), new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue("IntelliTect.Multitool.Testing"))));
    }
}
