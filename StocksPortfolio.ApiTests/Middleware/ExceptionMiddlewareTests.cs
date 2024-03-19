using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using StocksPortfolio.Domain.Entities;
using StocksPortfolio.Domain.Exceptions;
using Xunit;

namespace StocksPortfolio.Middleware.Tests;

public class ExceptionMiddlewareTests
{
    private Mock<ILogger<ExceptionMiddleware>> _loggerMock;
    private DefaultHttpContext _context;

    public ExceptionMiddlewareTests()
    {
        _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        _context = new DefaultHttpContext();

    }

    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextRequestDelegate()
    {
        var nextRequestDelegate = new Mock<RequestDelegate>();
        var middleware = new ExceptionMiddleware(nextRequestDelegate.Object, _loggerMock.Object);

        await middleware.InvokeAsync(_context);

        nextRequestDelegate.Verify(next => next.Invoke(_context), Times.Once);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCodeTo404()
    {
        var notFoundException = new NotFoundException(nameof(Portfolio), "object-id");
        var middleware = new ExceptionMiddleware(_ => throw notFoundException, _loggerMock.Object);

        await middleware.InvokeAsync(_context);

        _context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact()]
    public async Task InvokeAsync_WhenBadRequestExceptionThrown_ShouldSetStatusCodeTo400()
    {
        var exception = new BadRequestException("sth went wrong");
        var middleware = new ExceptionMiddleware(_ => throw exception, _loggerMock.Object);

        await middleware.InvokeAsync(_context);

        _context.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact()]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCodeTo500()
    {
        var exception = new Exception();
        var middleware = new ExceptionMiddleware(_ => throw exception, _loggerMock.Object);

        await middleware.InvokeAsync(_context);

        _context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
}