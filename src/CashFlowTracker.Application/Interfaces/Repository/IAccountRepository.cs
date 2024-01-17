﻿using CashFlowTracker.Domain.Entities;

namespace CashFlowTracker.Application.Interfaces.Repository
{
    public interface IAccountRepository : IEntityFrameworkBaseRepository<Account>
    {
    }
}