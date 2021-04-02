﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging.Azure.Attributes;
using Liquid.Messaging.Azure.Configuration;
using Liquid.Messaging.Azure.Tests.Messages;
using Liquid.Messaging.Configuration;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Liquid.Messaging.Azure.Tests.Consumers
{
    /// <summary>
    /// AzureServiceBusTestEventConsumer Class.
    /// </summary>
    /// <seealso cref="ServiceBusConsumer{AzureTestMessage}" />
    [ServiceBusConsumer("TestAzureServiceBus", "TestMessageTopic", "TestMessageSubscription")]
    public class ServiceBusTestConsumer : ServiceBusConsumer<ServiceBusTestMessage>
    {
        public override ServiceBusSettings AzureServiceBusSettings => new ServiceBusSettings { MaxConcurrentCalls = 3 };

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusTestConsumer" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="contextFactory">The context factory.</param>
        /// <param name="telemetryFactory">The telemetry factory.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="messagingSettings">The messaging settings.</param>
        public ServiceBusTestConsumer(IServiceProvider serviceProvider,
                                      IMediator mediator,
                                      IMapper mapper,
                                      ILightContextFactory contextFactory,
                                      ILightTelemetryFactory telemetryFactory,
                                      ILoggerFactory loggerFactory,
                                      ILightConfiguration<List<MessagingSettings>> messagingSettings) : base(serviceProvider, mediator, mapper, contextFactory, telemetryFactory, loggerFactory, messagingSettings)
        {
        }


        /// <summary>
        /// Consumes the message from  subscription asynchronous.
        /// </summary>
        /// <param name="message">The message to be consumed.</param>
        /// <param name="headers">The custom headers of message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override async Task<bool> ConsumeAsync(ServiceBusTestMessage message, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            ServiceBusTestMessage.Self = message;
            return await Task.FromResult(true);
        }
    }
}