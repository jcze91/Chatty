﻿using System.ServiceModel;

namespace Service.Contracts
{
    [ServiceContract]
    public interface UserContract : IRepository<int, Models.User>
    {
    }
}