using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common
{
    public enum OperationCode:byte
    {
        Login,
        Register,
        Default,
        SyncPosition,
        SyncPlayer,
    }

    public enum  EventCode:byte
    {
        SyncPlayer,
        SyncPosition,
    }
    public enum ParameterCode : byte {
        Username ,
        Password ,
        X,Y,Z,
        SyncPlayerList,
        SyncPositionList,
    }
    public enum ResultCode : short {
        Success,
        Failed,
    }
    
}
