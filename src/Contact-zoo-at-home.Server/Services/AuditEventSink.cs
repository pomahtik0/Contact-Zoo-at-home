// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Server.Services
{
    public class AuditEventSink : DefaultEventSink
    {
        public AuditEventSink(ILogger<DefaultEventService> logger) : base(logger)
        {
        }

        public override Task PersistAsync(Event evt)
        {
            return base.PersistAsync(evt);
        }
    }
}







