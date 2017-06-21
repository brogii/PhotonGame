using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Common
{   [ProtoContract]
    public  class PlayerPosition
    {   [ProtoMember(1)]
        public PositionData PositionData { get; set; }
        [ProtoMember(2)]
        public string Username { get; set; }
    }
}
