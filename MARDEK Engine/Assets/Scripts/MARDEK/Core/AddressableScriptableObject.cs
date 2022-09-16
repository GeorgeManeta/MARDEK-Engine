using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;

namespace MARDEK.Core
{
    // Comment the following line  to make JSONReader.cs work:
    [fsObject(Converter = typeof(GuidReferenceConverter))]
    public abstract class AddressableScriptableObject : ScriptableObject, IAddressableGuid
    {
        public Guid GetGuid()
        {
            return AddressableDatabase.GetGUID(this);
        }
    }
}
