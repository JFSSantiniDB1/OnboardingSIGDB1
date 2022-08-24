using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Dto;
using Xunit;

namespace OnboardingSIGDB1.Test.Util;

public static class AssertExtension
{
    public static void ComMensagemEsperada(this Notification notification, string mensagem)
    {
        if(notification.Message == mensagem)
            Assert.True(true);
        else
            Assert.False(true, $"Esperava a mensagem '{mensagem}'");
    }
    
    public static void ComMensagemEsperada(this ValidationFailure? validation, string mensagem)
    {
        if(validation?.ErrorMessage == mensagem)
            Assert.True(true);
        else
            Assert.False(true, $"Esperava a mensagem '{mensagem}'");
    }
}