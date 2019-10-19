﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Core
{
    [DataContract(Namespace = "MovieDb")]
    public class EntityBase : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
