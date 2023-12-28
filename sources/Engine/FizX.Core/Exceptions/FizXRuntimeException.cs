using System;

namespace FizX.Core.Exceptions;

public class FizXRuntimeException : Exception
{
    public FizXRuntimeException()
    {
        
    }

    public FizXRuntimeException(string message) : base(message)
    {
        
    }
}