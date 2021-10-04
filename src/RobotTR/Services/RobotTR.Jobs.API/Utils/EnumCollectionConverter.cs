using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTR.Jobs.API.Utils
{
    public class EnumCollectionConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
    {
        public EnumCollectionConverter() : base(
          v => JsonConvert
            .SerializeObject(v.Select(e => e.ToString()).ToList()),
          v => JsonConvert
            .DeserializeObject<ICollection<string>>(v)
            .Select(e => (T)Enum.Parse(typeof(T), e)).ToList())
        {
        }
    }
}