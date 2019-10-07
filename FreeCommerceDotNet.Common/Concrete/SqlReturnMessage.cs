namespace FreeCommerceDotNet.Common.Concrete
{
    public enum SqlReturnMessage
    {
        SqlError=0,
        ParameterError=1,
        AlreadyExists=2,
        NotFound=3,
        ForeignKeyError=4,
        Success=5
        
    }

    public enum ServiceReturn
    {
        ParameterError,
        Success

    }
}