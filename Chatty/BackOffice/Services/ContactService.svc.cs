﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BackOffice.Services
{
    public class ContactService : Utils.BaseService<int, Dbo.Contact, DataAccess.ContactDao>, Contracts.ContactContract { }
}
