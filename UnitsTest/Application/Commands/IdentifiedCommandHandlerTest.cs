using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalletService.API.Application.Commands.IdentifiedCommands;
using WalletService.API.Application.Commands.WalletCommands;
using WalletService.Service.Infrastructure.Idempotency;
using Xunit;

namespace UnitsTest.Application.Commands
{
    public class IdentifiedCommandHandlerTest
    {
        private readonly Mock<IRequestManager> _requestManager;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<IdentifiedCommandHandler<CreateWalletCommand, bool>>> _loggerMock;

        public IdentifiedCommandHandlerTest()
        {
            _requestManager = new Mock<IRequestManager>();
            _mediator = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<IdentifiedCommandHandler<CreateWalletCommand, bool>>>();
        }

        [Fact]
        public async Task Handler_sends_command_when_wallet_no_exists()
        {
            // Arrange
            var fakeGuid = Guid.NewGuid();
            var fakeWalletCmd = new IdentifiedCommand<CreateWalletCommand, bool>(FakeWalletRequest(), fakeGuid);

            _requestManager.Setup(x => x.ExistAsync(It.IsAny<Guid>()))
               .Returns(Task.FromResult(false));

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<bool>>(), default(System.Threading.CancellationToken)))
               .Returns(Task.FromResult(true));

            //Act
            var handler = new IdentifiedCommandHandler<CreateWalletCommand, bool>(_mediator.Object, _requestManager.Object, _loggerMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeWalletCmd, cltToken);

            //Assert
            Assert.True(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<bool>>(), default(System.Threading.CancellationToken)), Times.Once());
        }

        [Fact]
        public async Task Handler_sends_no_command_when_wallet_already_exists()
        {
            // Arrange
            var fakeGuid = Guid.NewGuid();
            var fakeOrderCmd = new IdentifiedCommand<CreateWalletCommand, bool>(FakeWalletRequest(), fakeGuid);

            _requestManager.Setup(x => x.ExistAsync(It.IsAny<Guid>()))
               .Returns(Task.FromResult(true));

            _mediator.Setup(x => x.Send(It.IsAny<IRequest<bool>>(), default(System.Threading.CancellationToken)))
               .Returns(Task.FromResult(true));

            //Act
            var handler = new IdentifiedCommandHandler<CreateWalletCommand, bool>(_mediator.Object, _requestManager.Object, _loggerMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeOrderCmd, cltToken);

            //Assert
            Assert.False(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<bool>>(), default(System.Threading.CancellationToken)), Times.Never());
        }

        private CreateWalletCommand FakeWalletRequest(Dictionary<string, object> args = null)
        {
            return new CreateWalletCommand(
                userId: args != null && args.ContainsKey("userId") ? (Guid)args["userId"] : Guid.Empty);
        }
    }
}
